using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerControls : MonoBehaviour
{
    public ScoreListVariable scoreList;


    public float moveSpeed = 10f; //αρχικοποίηση της τιμής για την ταχύτητα κίνησης σε 10
    public float midAircontrolBack = 1f; //αρχικοποίηση της τιμής για έλεγχο κίνησης στον αέρα και πίσω
    public float midAircontrolFront = 4f; //αρχικοποίηση της τιμής για έλεγχο κίνησης στον αέρα και μπροστά
    public float jumpVelocity = 10f; //δινει μια τιμή ταχύτητας στο άλμα που πρόκειται να κάνουμε

    public float fallMultiplier = 2.5f; //αρχικοποίηση της τιμής για το πόσο γρήγορα θα πέφτει ο παίκτης απο το άλμα
    public float lowJumpMultiplier = 2f; //αρχικοποίηση της τιμής για το πόσο γρήγορα θα γίνονται τα μικρά άλματα

    public LayerMask boxLayerMask; //μας επιτρέπει να διαλέγουμε layer πιο μετά για το boxcast
    private Rigidbody2D player; //φτιάχνει μια μετάβλητή τύπου rigidbody2d ονόματι player
    private BoxCollider2D boxCollider2d; //φτιάχνει μια μετάβλητή τύπου boxCollider2D 

    public Text showLives; //δημιουργία τύπου Text μετάβλητής showLives
    public Text showScore; //δημιουργία τύπου Text μετάβλητής showScore
    public Text showHighScore; //δημιουργία τύπου Text μετάβλητής showHighScore

    public FloatVariable Lives; //δημιουργία τύπου FloatVariable μετάβλητής Lives
    public FloatVariable HighScore; //δημιουργία τύπου FloatVariable μετάβλητής HighScore
    public FloatVariable Score; //δημιουργία  τύπου FloatVariable μετάβλητής Score

    public GameObject gBox; //δημιουργία τύπου GameObject μετάβλητής gBox
    public Vector2 uPos; //δημιουργία τύπου Vector2 μετάβλητής uPos
    public Vector2 dPos; //δημιουργία τύπου Vector2 μετάβλητής dPos
    private int flagUp = 0, flagDown = 0; //αρχικοποίηση της int μετάβλητής flagUp και flagDown με την τιμή 0
    public float speed = 3; //αρχικοποίηση της float μετάβλητής speed με 2
    public float nextSwitch; //αρχικοποίηση της float μετάβλητής nextSwitch με 2
    public float switchRate = 1f; //αρχικοποίηση της float μετάβλητής switchRate με 1

    public StringVariable lane; //δημιουργία τύπου StringVariable μετάβλητής lane

    public float Bscale, Sscale;
    public Vector3 bigScale; //δημιουργία τύπου Vector3 μετάβλητής bigScale
    public Vector3 smallScale; //δημιουργία τύπου Vector3 μετάβλητής  smallScale
    public float animationSpeed; //δημιουργία τύπου  μετάβλητής animationSpeed

    private Vector3 velocity = Vector3.zero; //δημιουργία τύπου Vector3 μετάβλητής velocity και αρχικοποίηση της ως Vector3.zero

    private float slowdownFactor = 0.05f; //αρχικοποίηση της τιμής για το πόσο θα επιβραδύνει ο χρόνος με το slowdownFactor
    public int interval = 1; //αρχικοποίηση της int μετάβλητής interval με 1
    private float nextTime = 0f;  //αρχικοποίηση της float μετάβλητής nextTime με 0
    int dead = 0; // αρχικοποίηση της int μετάβλητής dead με 0

    private SpriteRenderer player_SpriteRenderer;
    public Animator animator;


    void Start()
    {

        player_SpriteRenderer = GetComponent<SpriteRenderer>(); // βρες το sprite του παίκτη

       
        dead = 0; //αρχικοποίηση της dead = 0 μέσα στο Start

        nextTime = 0f; //επόμενος χρόνος = 0
        Lives.value = 2; //οι ζωές είναι ίσες με 2
        Score.value = 0; // το τωρινό σκορ είναι ίσο με 2
        HighScore.value = scoreList.highscores[scoreList.highscores.Count - 1].playerScore;
        showHighScore.text = "High Score : " + HighScore.value.ToString(); //εμφάνισε κείμενο στον καμβά το οποίο θα γράφει High Score : και δίπλα το high score που έχει επιτεθεί στο παιχνίδι

        player = transform.GetComponent<Rigidbody2D>(); //αρχικοποίηση του χαρακτήρα ως στερεό σώμα
        boxCollider2d = transform.GetComponent<BoxCollider2D>(); //δίνει στο χαρακτήρα ένα "κουτί ελέγχου" το οποίο αργότερα
        //θα μας επιτρέψει να ελένξουμε αν ο χαρακτήρας πηδάει ή όχι


        this.transform.localScale = bigScale; //το μέγεθος αυτόύ του αντικείμενου, είναι ίσο με το bigScale άρα την μεγάλη κλίμακα
        lane.txt = "EnemyA"; //η μετάβλητή lane.txt παίρνει την τιμή EnemyA για να ξέρουμε ότι βρισκόμαστε στην λωρίδα 1

        animator.SetBool("Up" , false);
        animator.SetBool("Down" , false);
        animator.SetBool("Jump" , false);
        
    }

    private void Update()
    {
        // CHEATS :
        if (Input.GetKey(KeyCode.L)) //delete current highscore 
            {
            HighScore.value = 0; 
            }
        if (Input.GetKey(KeyCode.K)) // become dead
        {
            Lives.value = -1f;
            showLives.text = "Lives : " + Lives.value;
        }
        if (Input.GetKey(KeyCode.P)) //get 100 lives
        {
            Lives.value = 100;
            showLives.text = "Lives : " + Lives.value;
        }
        // END CHEATS


        if (dead == 0) //αν το dead είναι ίσο με το 0, δηλαδή ο παίκτης μας είναι ζωντανός τότε
        {
            HandleMovement(); //τρέξε το handlemovement
            LaneSwitch(); //τρέξε το LaneSwitch
            if (IsGrounded() && Input.GetButton("Jump") && flagUp == 0 && flagDown == 0)//αν είσαι στο έδαφος ΚΑΙ πατήσεις το κουμπί για το άλμα που έχουμε ορίσει απο τα settings ΚΑΙ το flagUp ΚΑΙ το flagDown είναι 0 τότε 
            {
                animator.SetBool("Jump" , true);
                SoundManager.PlaySound("playerJump");
                player.velocity = Vector2.up * jumpVelocity; //επιτάχυνε τον χαρακτήρα προς τα πάνω κατά jumpVelocity		
            }

            if (IsGrounded() == true)
            {
                animator.SetBool("Jump" , false);
            }
            else
            {
                animator.SetBool("Jump" , true);
            }

            JumpSmooth(); //κώδικας για να γίνει καλύτερο το άλμα, θα το δούμε στη συνέχεια



            if (nextTime <= 0)   //if (Time.time >= nextTime) //ανά δευτερόλεπτο
            {
                player_SpriteRenderer.color = Color.white; // επανέφερε το χρώμα σε άσπρο

                Score.value++; //κάθε δευτερόλεπτο πρόσθεσε +1 σκορ
                if (Score.value > HighScore.value) //αν ποτέ το σκορ είναι μεγαλύτερο από το HighScore.value τότε 
                {
                    HighScore.value = Score.value; //το high score είναι ίσο με το τωρινό σκορ
                    showHighScore.text = "High Score : " + HighScore.value.ToString(); //κάνε ανανέωση το κείμενο για να δείξεις στον παίκτη το highscore
                }

                showScore.text = "Score : " + Score.value.ToString(); //δείξε στον παίκτη το σκορ του

                nextTime = interval; //επομένη χρονική στιγμή οπού θα γίνει γεγονός είναι ίση με interval
            }
            else
            {
                nextTime -= Time.deltaTime; //αφαίρεσε 1 για κάθε δευτερόλεπτο που περνάει από το nextTime
            }
        }

    }

    void JumpSmooth()
    {
        if (player.velocity.y < 0)
        { //αν η ταχύτητα του παίκτη είναι αρνητική, δηλαδή ο παίκτης αρχίσει να πέφτει, τότε
            

            player.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime; //η ταχύτητα του παίκτη είναι ίση με την βαρύτητα που έχουμε ορίσει, μια έξτρα μετάβλητή αύξησης ταχύτητας πτώσεως που ορίσαμε ιν
        }
        else if (player.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        { //αλλιώς αν η ταχύτητα είναι θετική και το SPACE 
          //είναι κρατημένο τότε
            player.velocity += Vector2.up * Physics2D.gravity * Time.deltaTime; //η ταχύτητα είναι πολλαπλάσια της βαρύτητας
        }
    }

    private bool IsGrounded()
    { //ελέγχει αν είναι στο έδαφος ή όχι, γιαυτό είναι boolean. Tο raycast είναι ένα function του συστήματος που μας επιτρέπει να ελέγχουμε διάφορες θέσης των αντικειμένων μέσα σε μια σκηνή. Ουσιαστικά καλούμε το Raycast αλλά επειδή έχουμε να κάνουμε με παραπάνω από 1 pixel αλλά με κουτί, θα πρέπει να καλέσουμε το boxcast για να ελέγχουμε από όλο το κουτί του χαρακτήρα

        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center/*κέντρο κουτιού*/, boxCollider2d.bounds.size/*μέγεθος*/, 0f/*γωνία*/, Vector2.down/*που δείχνει*/ , .1f/*πόσο πολύ δείχνει*/, boxLayerMask /*τι να δείχνει*/);
        return raycastHit2d.collider != null; //γυρνά μια τιμή αν το boxcast αγγίζει ένα boxLayerMask τύπου αντικείμενο
    }

    private void HandleMovement()
    { //εδώ θα δούμε τη κίνηση του χαρακτήρα καθ' όλη τη διάρκεια του παιχνιδιού

        if (Input.GetKey(KeyCode.LeftArrow))
        { //αν πατηθεί το αριστερό βελάκι , τότε
            if (IsGrounded())
            { //αν βρίσκετε στο έδαφος και πατηθεί το αριστερό βελάκι , τότε
                player.velocity = new Vector2(-moveSpeed , player.velocity.y); //δώσε ταχύτητα αρνητική και ίση με το moveSpeed που ορίσαμε αρχικά για να κινείται αριστερά

            }
            else
            {
                player.velocity += new Vector2(-moveSpeed * midAircontrolBack * Time.deltaTime , 0); //αν είναι στον αέρα τότε η ταχύτητα είναι πολλαπλάσια με το midAircontrolBack το οποίο ορίσαμε πριν, με αυτό το τρόπο κάνουμε το άλμα προς τα πίσω να είναι πιο "δύσκαμπτο"
                player.velocity = new Vector2(Mathf.Clamp(player.velocity.x , -moveSpeed , +moveSpeed) , player.velocity.y);
                //αυτή η συνάρτηση μας επιτρέπει να έχουμε ελάχιστα και μέγιστα για τις ταχύτητες μας, έτσι ώστε να μην ξεπερνούν ποτέ την ταχύτητα που ο παίκτης έχει στο έδαφος.
            }
        }
        else
        {

            if (Input.GetKey(KeyCode.RightArrow))
            { //η παρακάτω συνάρτηση είναι η ίδια με αυτλη για την κίνηση στα αριστερά, αλλά για προς τα δεξιά
                if (IsGrounded())
                {
                    player.velocity = new Vector2(+moveSpeed , player.velocity.y);
                }
                else
                {

                    player.velocity += new Vector2(+moveSpeed * midAircontrolFront * Time.deltaTime , 0);
                    player.velocity = new Vector2(Mathf.Clamp(player.velocity.x , -moveSpeed , +moveSpeed * 2) , player.velocity.y);


                }
            }
            else
            {
                if (IsGrounded())
                { //αν ο χαρακτήρας είναι ακίνητος τότε
                    player.velocity = new Vector2(0 , player.velocity.y); //η ταχύτητα του είναι = 0
                }
            }
        }
    }

    //σύστημα ελέγχου της 'ζωής" του χαρακτήρα
    void OnTriggerStay2D(Collider2D other) // αν συγκρουστεί με
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(lane.txt))
        {
            SoundManager.PlaySound("playerHit");
            if (Lives.value > 0) //εάν έχει ζωές
            {
                Lives.value -= 1; // αφαιρεί μία
                showLives.text = "Lives : " + Lives.value;


                player_SpriteRenderer.color = Color.yellow;
                
            }

            else if (Lives.value <= 0) //αλλιώς ο παίκτης πεθαίνει
            {
                dead = 1;
                timeSlow();
                StartCoroutine(ExecuteAfterTime(0.2f));                
            }
        }
    }

    private void LaneSwitch()
    {//εδώ η θα δώσουμε στον παίκτη μας την ικανότητα να μπορεί να κινητέ στο "βάθος"
        if (IsGrounded() && Input.GetKey(KeyCode.UpArrow) && Time.time > nextSwitch && gBox.transform.position.y != uPos.y) //αν ο χαρακτήρας είναι στο έδαφος ΚΑΙ πατηθεί το πάνω βελάκι ΚΑΙ ο χρονος είναι είναι μεγαλυτερος απο το χρόνο εναλλαγής nextSwitch ΚΑΙ η θέση του gBox είναι διάφορη από την uPos (μεγίστη) στον άξονα y, τότε
        {
            flagUp = 1; //κάνε το flagUp ίσο με το 1
            nextSwitch = Time.time + switchRate; //και βάλε το nextSwitch να είναι ίσο με την τωρινή χρονική στιγμή + την τιμή που έχουμε ορίσει εμείς στο switchRate
        }

        if (gBox.transform.position.y < uPos.y && flagUp == 1) //Αν η θέση του gBox στον άξονα y είναι μικρότερη από το uPos στον άξονα y (δηλαδή τη μέγιστη τιμή στο y) ΚΑΙ το flagUp είναι ίσο με 1 τότε
        {
            lane.txt = "NULL"; //η τιμή του lane.txt είναι ίση με "NULL"
            Scaling("small"); //καλούμε την Scaling με όρισμα small
            animator.SetBool("Up" , true);
            gBox.transform.position = Vector2.MoveTowards(gBox.transform.position , uPos , speed * Time.deltaTime % 1f); //το gBox θα κινηθεί ανάμεσα στις συντεταγμένες της τωρινής του θέσης και της uPos με ταχύτητα = speed * Time.deltaTime για να γίνεται κάθε δευτερόλεπτο

            if (gBox.transform.position.y == uPos.y) //αν η θέση του g.Box στον άξονα y είναι ίση με το uPos ,δηλαδή φτάσει στη μεγίστη τιμή, τότε 
            {
                animator.SetBool("Up" , false);
                lane.txt = "EnemyB"; //το lane.txt θα πάρει την τιμή "EnemyB"
                flagUp = 0; // και το flagUp θα γίνει ίσο με το 0
            }
        }

        if (IsGrounded() && Input.GetKey(KeyCode.DownArrow) && Time.time > nextSwitch && gBox.transform.position.y != dPos.y) //αν ο χαρακτήρας είναι στο έδαφος ΚΑΙ πατηθεί το κουμπί κάτω ΚΑΙ ο χρόνος είναι είναι μεγαλύτερος από το χρόνο εναλλαγής nextSwitch ΚΑΙ η θέση του gBox είναι διάφορη του dPos (ελάχιστη) στον άξονα y, τότε
        {
            flagDown = 1; //κάνε το flagDown ίσο με 1
            nextSwitch = Time.time + switchRate; //και βάλε το nextSwitch να είναι ίσο με την τωρινή χρονική στιγμή + την τιμή που έχουμε ορίσει εμείς στο switchRate
        }

        if (gBox.transform.position.y > dPos.y && flagDown == 1) //Αν η θέση του gBox στον άξονα y είναι μεγαλύτερη από το dPos στον άξονα y (δηλαδή τη ελαχιστη τιμή στο y) ΚΑΙ το flagUp είναι ίσο με 1 τότε
        {
            lane.txt = "NULL"; //η τιμή του lane.txt είναι ίση με "NULL"
            Scaling("big"); //καλούμε την Scaling με όρισμα big
            animator.SetBool("Down" , true);
            gBox.transform.position = Vector2.MoveTowards(gBox.transform.position , dPos , speed * Time.deltaTime % 1f); //το gBox θα κινηθεί ανάμεσα στις συντεταγμένες της τωρινής του θέσης και της dPos με ταχύτητα = speed * Time.deltaTime για να γίνεται κάθε δευτερόλεπτο

            if (gBox.transform.position.y == dPos.y) //αν η θέση του g.Box στον άξονα y είναι ίση με το uPos ,δηλαδή φτάσει στην ελάχιστη τιμή, τότε 
            {
                {
                    lane.txt = "EnemyA"; //το lane.txt θα πάρει την τιμή "EnemyA"
                    flagDown = 0; //και το flagDown θα γίνει ίσο με το 0
                    animator.SetBool("Down" , false);
                }
            }

        }
    }

    private void Scaling(string x)
    {//το function αυτό θα το χρησιμοποιήσουμε για να κάνουμε τον παίκτη να μικραίνει και να μεγαλώνει έτσι ώστε να δίνει την ψευδαίσθηση κίνησης στο βάθος

        if (x == "big" && this.transform.localScale.y < Bscale ) //αν έχει δοθεί τιμή big στο κάλεσμα της Scaling ΚΑΙ η κλίμακα του αντικείμενου αυτόύ δεν υπερβαίνει το 1 στον άξονα y τότε
        {
           
            this.transform.localScale = new Vector3(this.transform.localScale.x + 0.02f , this.transform.localScale.y + 0.02f , this.transform.localScale.z); //αύξησε την τωρινή του κλίμακα κατά 0.02f κάθε frame 
        }

        if (x == "small" && this.transform.localScale.y > Sscale) //αν έχει δοθεί τιμή small στο κάλεσμα της Scaling ΚΑΙ η κλίμακα του αντικείμενου αυτόύ δεν ξεπερνά το 1 στον άξονα y τότε
        {           
            this.transform.localScale = new Vector3(this.transform.localScale.x - 0.02f , this.transform.localScale.y - 0.02f , this.transform.localScale.z); ////μείωσε την τωρινή του κλίμακα κατά 0.02f κάθε frame 
        }

    }

    private void timeSlow() //function η οποία θα μας δώσει το εφέ του χρόνου που κυλάει αργά
    {

        Time.timeScale = slowdownFactor; //ο τωρινός χρόνος είναι ίσο με τη μετάβλητή επιβράδυνσης slowdownFactor
        Time.fixedDeltaTime = Time.timeScale * .02f; //ο χρόνος είναι ίσος με την κλίμακα του χρόνου * 0.2f για να μας δώσει την αίσθηση του αργού χρόνου
    }

    IEnumerator ExecuteAfterTime(float time) //function εκτέλεσης εντολών μετά από time αριθμό χρόνου
    {
        yield return new WaitForSeconds(time); // εντολή που περιμένει time χρόνο
        Time.timeScale = 1; //κάνει reset το xrono στο normal μετά το timeSlow
        SceneManager.LoadScene("Death"); //φόρτωση σκηνής RestartMenu

    }   
   
}




