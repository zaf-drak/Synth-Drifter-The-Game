using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacking : MonoBehaviour
{
    public Transform attackPoint; //ορίζουμε το χώρο που θα καταλάβει το attackPoint δηλαδή η περιοχή χτυπήματος
    public float range = 0.1f; //ορίζουμε την ακτίνα έλεγχου

    public float attackRate = 1f; //όρισμα ορίου χτυπήματος του παίκτη για να μην μπορεί να κάνει συνέχεια επιθέσεις
    public float nextAttack; //όρισμα πότε θα είναι το επόμενο χτύπημα διαθέσιμο στο παίκτη

    public FloatVariable Score; //εισαγωγή της δημόσιας και τύπου FloatVariable μεταβλητής Score
    public LayerMask enemyLayerMask; //ορίζουμε μια μετάβλητή η οποία θα αντιπροσωπεύει το layer που θέλουμε να ελέγξουμε

    public StringVariable lane; //εισαγωγή της δημοσίας μεταβλητής τύπου StringVariable μεταβλητής lane

    public FloatVariable Lives; //εισαγωγή της δημοσίας μεταβλητής τύπου FloatVariable μεταβλητής Lives

    private float lifeUp = 0; //όρισμα και αρχικοποίηση του μετρητή lifeUp για να μπορέσει ο παίκτης μας να αρχίσει να παίρνει ζωές μόνος του.

    public Text showLives; //ορίζουμε την τύπου Text μετάβλητή showLives για να δείξουμε στον παίκτη τις ζωές που έχει εφόσον τις έχει αυξήσει

    public SpriteRenderer sprite;
    SpriteRenderer enemy_SpriteRenderer;


    private Animator anim;
    public Animator animator;

    void Start()
    {
        animator.SetBool("Attack" , false);

    }

    void Update()
    {
        
        if (Time.time > nextAttack)
        {
            //sprite.color = Color.green;
            //animator.SetBool("Attack" , false);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Time.time > nextAttack) //αν το κουμπί που πατηθεί είναι το αριστερό Ctrl και έχει περάσει χρόνος περισσότερος από το nextAttack value τότε
        {
            animator.SetBool("Attack" , true);
            SoundManager.PlaySound("swish1");
            //sprite.color = Color.red;
            nextAttack = Time.time + attackRate; //η επομένη επίθεση που θα μπορεί να κάνει ο παίκτης είναι ίση με το χρόνο που πατήθηκε το κουμπί + την τιμή του attackRate           

            attack(); //κάλεσμα της attack function 
        }
    }

    void attack()
    {
       
        LayerMask enemyLayerMask = LayerMask.GetMask(lane.txt);
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position , range , enemyLayerMask);
        

        //δημιουργούμε μια λίστα Collider2D με όνομα enemiesHit το οποίο ορίζουμε εδώ για πρώτη φορά, το οποίο είναι ίσο με ένα κύκλο ο οποίος έχει ακτίνα = attackPoint, απόσταση = range και ελέγχει το enemyLayerMask

        foreach (Collider2D enemy in enemiesHit) //βάζει ό,τι βρήκε με το OverlapCircleAll σε μια λίστα enemiesHit και το ονομάζει enemy
        {
            if (enemy.name.Contains("Enemy 1"))
            {
                Score.value += 5; //πρόσθεσε 5 στο σκορ αυξάνοντας το έτσι πολύ πιο γρήγορα
                lifeUp += 5;
                SoundManager.PlaySound("enemyDeath1");
            }
            else if (enemy.name.Contains("Enemy 2"))
            {
                Score.value += 8;
                lifeUp += 8;
                SoundManager.PlaySound("enemyDeath2");
            }
            else if (enemy.name.Contains("Enemy 3"))
            {
                Score.value += 3;
                lifeUp += 3;
                SoundManager.PlaySound("enemyDeath3");
            }


           
            anim = enemy.GetComponent<Animator>();
            enemy.GetComponent<EnemyBehavior>().speedHitBool = true;
            anim.SetBool("Is_Hit", true);
            Destroy(enemy); //κατάστρεψε το BoxCollider2D του κάθε αντικειμένου enemy για να μην μπορεί να βλάψει το χρήστη
        }

        if (lifeUp >= 50) //αν ποτέ η μετάβλητή lifeUp είναι μεγαλύτερη η και ίση του 50 τότε
        {
            Lives.value += 1; //αύξησε τις ζωές του παίκτη μέσω της Lives.value
            showLives.text = "Lives : " + Lives.value; //δείξε στον παίκτη πόσες ζωές έχει τώρα μέσο text 
            lifeUp = 0; //μηδένισε το μετρητή
        }

        
    }

    void attackEnd()
    {
        animator.SetBool("Attack" , false);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position , range);
    }

   
}

