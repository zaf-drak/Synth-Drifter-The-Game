using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public FloatVariable Score; //δηλώνουμε την FloatVariable τύπου μετάβλητή με όνομα Score
    public float increment = 25; //ανά πόσο Score θέλουμε να αυξάνετε η ταχύτητα
    public SpeedControl SpeedControl; //δήλωση μεταβλητής SpeedControl τύπου SpeedControl

    void Start()
    {
        SpeedControl.reset(); //εδώ καλούμε μεσα απο το script SpeedControl την κλάση reset η οποία μηδενίζει τις ταχύτητες μας.
    }


    void Update()
    {
        if (Score.value > increment && increment < 1000) //αν το σκορ μας είναι μεγαλύτερο απο το increment ΚΑΙ το increment είναι μικρότερο από 1000 τότε
        {
            SpeedControl.bgIncrease(1); //αύξηση της ταχύτητας του background καλώντας την bgIncrease από το SpeedControl και δίνοντας της όρισμα 1. Άρα η ταχύτητα των παρασκήνιων αυξάνετε κατά 1
            SpeedControl.enemyIncrease(1 , 1); //αύξηση ταχύτητας των εχθρών καλώντας την enemyIncrease από το SpeedControl και δίνοντας 1 και 1 ορίσματα. Το πρώτο όρισμα είναι για το πιο είδους εχθρόύ θέλουμε να επιταχύνει και το δεύτερο όρισμα είναι κατά πόσο
            SpeedControl.enemyIncrease(2 , 1);
            SpeedControl.enemyIncrease(3 , 1);
            increment += 25;
            //αύξηση του μετρητή increment κατά 25 έτσι ώστε το παιχνίδι κάθε 25 Score να γίνετε πιο δύσκολο
        }
    }

}
