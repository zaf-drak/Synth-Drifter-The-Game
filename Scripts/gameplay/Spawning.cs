using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{

    public GameObject enemy1; //δημιουργία τύπου GameObject μεταβλητής enemy1
    public GameObject enemy2; //δημιουργία τύπου GameObject μεταβλητής enemy2
    public GameObject enemy3; //δημιουργία τύπου GameObject μεταβλητής enemy3
    public GameObject enemy4; //δημιουργία τύπου GameObject μεταβλητής enemy4

    private int countdown; //δημιουργία τύπου int μεταβλητής countdown
    private float rng; //δημιουργία τύπου float μεταβλητής rng
    private float time2Spawn; //δημιουργία τύπου float μεταβλητής time2Spawn
    public float startSpawn;  //δημιουργία τύπου float μεταβλητής startSpawn
    public float minSpawn = 1; //δημιουργία και αρχικοποίηση της τύπου float μεταβλητής minSpawn
    public float maxSpawn = 3; //δημιουργία και αρχικοποίηση της τύπου float μεταβλητής maxSpawn
    public float increment = 25; //δημιουργία και αρχικοποίηση της τύπου float μεταβλητής increment
    public Vector2 pos; //δημιουργία τύπου Vector2 μεταβλητής pos
    public FloatVariable Score;  //δημιουργία τύπου FloatVariable μεταβλητής Score
    public FloatVariable ChangusFlag;

    // Start is called before the first frame update
    void Start()
    {
        ChangusFlag.value = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (Score.value > increment && increment < 1000) //Αν τωρινό σκορ είναι μεγαλύτερο από το increment και το increment είναι μικρότερο του 1000 τότε
        {
            if (minSpawn > 0.3 && increment < 200) //αν το minSpawn είναι μεγαλύτερο από το 0.3 και το increment είναι μικρότερο από το 200 τότε
            {
                minSpawn -= (increment / 25) / 8; //το minSpawn και από κάτω το maxSpawn μειώνετε κατά το (1/25)/8 του increment
                maxSpawn -= (increment / 25) / 8;
            }

            if (minSpawn > 0.3 && increment > 200) //αν το minSpawn είναι μεγαλύτερο από το 0.3 και το increment είναι μεγαλυτερο από το 200 τότε
            {
                minSpawn -= (increment / 25) / 4; //το minSpawn και από κάτω το maxSpawn μειώνετε κατά το (1/25)/4 του increment
                maxSpawn -= (increment / 25) / 4;
            }

            increment += 25; //αύξηση του μετρητή increment κατά 25

        }

        if (time2Spawn <= 0) //αν ο χρόνος παράγωγης εχθρού είναι μικρότερος ή ίσος με το 0 τότε
        {
            pos = new Vector2(transform.position.x , transform.position.y + Random.Range(-0.05f , 0.05f)); //η μεταβλητή pos θα πάρει συντεταγμένες σύμφωνα με την αυτές του αντικείμενου οι οποία είναι σταθερή στον άξονα y αλλά παίρνει μια τυχαία τιμή από -0,7 έως 0,07 στον άξονα y όπου και προστίθεται
            startSpawn = Random.Range(minSpawn , maxSpawn); //ο χρόνος που θα παραχθεί ο εχθρός είναι ίσος με μια τυχαία τιμή μεταξύ του ελάχιστου χρόνου minSpawn και του μεγίστου χρόνου maxSpawn
            countdown--; //μείωση του μετρητή countdown
            if (countdown == 0) //αν ο μετρητής countdown είναι ίσος με το 0 τότε κάνε το ChangusFlag.value=0
            {
                ChangusFlag.value = 0;
            }

        start: //ένα από τα μέρη της εντολής goto, εδώ θα πάει ο κώδικας όταν δούμε το goto start
            rng = Random.value; // η μεταβλητή rng παίρνει μια τυχαία τιμή από το 0 μέχρι το 1

            if (rng < 0.3) //αν η rng είναι μικρότερη απο 0.3 τότε
            {
                Instantiate(enemy1 , pos , Quaternion.identity); //εμφάνισε τον enemy1 στην θέση pos, χωρίς περιστροφή
                time2Spawn = startSpawn; //επόμενος χρόνος παραγωγής εχθρού θα είναι ίσως με τον χρόνο παραγωγής startSpawn που έχουμε ορίσει
            }

            else if (rng < 0.6) //κάνει ακριβώς ότι και από πάνω μονό που εμφανίζει enemy2
            {
                Instantiate(enemy2 , pos , Quaternion.identity);
                time2Spawn = startSpawn;
            }

            else if (rng < 0.9) //κάνει ακριβώς ότι και από πάνω μονό που εμφανίζει enemy3
            {
                Instantiate(enemy3 , pos , Quaternion.identity);
                time2Spawn = startSpawn;
            }

            else
            {
                if (ChangusFlag.value == 0) //αν δεν πέσει σε κανένα απο τα προηγούμενα if τότε μπαίνει εδώ και ελέγχει αν το ChangusFlag.value είναι ίσο με το 0, αν είναι τότε
                {
                    Instantiate(enemy4 , pos , Quaternion.identity); //εμφανίζει τον εχθρό 4 όπως είδαμε και στα προηγούμενα Instantiate
                    time2Spawn = startSpawn; //προχωράει τον επόμενο χρόνο παραγωγής όπως προηγούμενος
                    ChangusFlag.value = 1; //κάνει το ChangusFlag.value ίσο με 1
                    countdown = 10; //θέτει το μετρητή countdown ίσο με 10
                }

                else if (ChangusFlag.value == 1) //αν το ChangusFlag.value είναι 1 τότε
                {
                    goto start; //πήγαινε στο start και συνέχισε τον κωδικά από εκεί
                }
            }

        }
        else
        {
            time2Spawn -= Time.deltaTime; //ο χρόνος για την παραγωγή εχθρού μειώνετε όσο περνάει ο χρόνος
        }
    }
}

