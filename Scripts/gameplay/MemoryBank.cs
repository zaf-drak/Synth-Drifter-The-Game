using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.IO;

public class MemoryBank : MonoBehaviour
{


    public static MemoryBank mBank; //δημιουργία της τύπου MemoryBank μετάβλητής mBank 
                                    //public float lives;
                                    //public float score;
    public float highScore; //δημιουργία μετάβλητής τύπου float, highScore

    //public FloatVariable Lives;
    public FloatVariable HighScore; //δημιουργία τύπου FloatVariable μετάβλητής HighScore
    //public FloatVariable Score;

    public string path; //δημιουργία τύπου string μετάβλητής path

    void Awake() //η Awake καλείτε όταν ένα ενεργό αντικείμενο το οποίο περιέχει το σκριπτ, δημιουργείτε όταν φορτώνετε μια σκηνή
    {
        path = Path.Combine(Application.persistentDataPath , "playerInfo.dat"); //η μετάβλητή path περνει ως τιμή το directory του αρχείου playerInfo.dat
        if (mBank == null) //αν η τράπεζα μνήμης είναι κενή τότε
        {
            DontDestroyOnLoad(gameObject); //μην το καταστρέψεις αυτό το αντικείμενο
            mBank = this; //η τράπεζα μνήμης παίρνει τα δεδομένα του αντικείμενου που βρίσκετε
        }
        else if (mBank != this) //αλλιώς αν η τράπεζα μνήμης δεν είναι αυτό το αντικείμενο
        {
            Destroy(gameObject); //κατάστρεψε αυτό το αντικείμενο
        }
    }



    public void Save() //function Save η οποία θα έχει ως βάση να αποθηκεύει πράγματα τα οποία θέλουμε να αποθηκευτούν για όταν ξανατρέξουμε το παιχνίδι
    {

        BinaryFormatter bf = new BinaryFormatter(); //δημιουργία μετάβλητής bf η οποία θα μετάτρέψει το αρχεία που θέλουμε να αποθηκεύσουμε σε serialized αρχεία

        FileStream file = File.Create(path); // δημιουργία τύπου FileStream μετάβλητής file η οποία θα έχει ως περιεχόμενο το directory του αρχείου που δημιουργείτε

        //(Application.persistentDataPath + "/playerInfo.dat");
        // save the sweet sweet stuff
        PlayerData data = new PlayerData(); //δημιουργία τύπου PlayerData μετάβλητής data η οποία θα περιέχει PlayerData

        //data.lives = lives;
        data.highScore = HighScore.value; //το τωρινό highscore είναι ίσο με το HighScore που ήταν και πριν το save
        bf.Serialize(file , data); //κάνε serialize το αρχείο
        file.Close(); //κλείσε το αρχείο που κάνεις μετάτροπές
    }

    [Serializable]
    class PlayerData //κλάση PlayerData που θα περιέχει το high score μας η οποία θα γίνει serialized για να μπορούμε να την αποθηκεύσουμε
    {
        //public float lives;
        //public float score = 0;
        public float highScore; //μετάβλητή float highScore 
    }


    public void Load() //function Save η οποία θα έχει ως βάση να φορτώσει το αρχείο το οποίο αποθηκεύσαμε με τη function Save για να μπορούμε να διατηρήσουμε τα δεδομένα μας
    {
        if (File.Exists(path)) // αν υπάρχει το αρχείο στο path = playerInfo.dat τότε
                               //(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter(); //πάρε τα αρχεία που έχουν αποθηκευτεί σε serialized μορφή
            FileStream file = File.Open(path , FileMode.Open); //άνοιξε το αρχείο αυτό
                                                               //(Application.persistentDataPath + "playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file); //φόρτωσε τα δεδομένα στο data 
            file.Close(); //κλείσε το αρχείο

            // fortosh apo to arxeio
            //lives = data.lives;
            HighScore.value = data.highScore; //κάνε το HighScore ίσο με το highScore που είχε αποθηκευτεί
        }
    }
}
