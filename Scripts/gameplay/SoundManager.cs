using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour 
{

    public static AudioClip swingSound, enemy1Death, enemy2Death, enemy3Death, playerDeath, playerJump, playerHit; //δηλώσεις όλων των πιθανών μεταβλητών που θα χρειαστούμε για διαφορετικούς ήχους

    static AudioSource source; //δήλωση AudioSource τύπου μεταβλητή 

    void Start() //αντιστοιχήσεις των μεταβλητών που δηλώσαμε πριν με το path καθως και με το όνομα των αρχειων ήχου που θέλουμε να φορτώσουμε στις μεταβλητές αυτές
    {
        swingSound = Resources.Load<AudioClip>("swish1");
        enemy2Death = Resources.Load<AudioClip>("enemyDeath1");
        enemy3Death = Resources.Load<AudioClip>("enemyDeath2");
        enemy1Death = Resources.Load<AudioClip>("barrelHit");
        playerJump = Resources.Load<AudioClip>("playerJump");
        playerHit = Resources.Load<AudioClip>("playerHit");

        source = GetComponent<AudioSource>(); //εντολή για να πάρει το Object που χρησιμοποιεί αυτό το Script το AudioSource και να το χρησιμοποιήσει
    }

    public static void PlaySound(string clip) //Function για να παίξει τον ήχο όταν το ζητήσουμε
    {
        switch (clip) //όλες οι πιθανές περιπτώσεις για sound effects που μπορεί να δώσουμε απο αλλα scripts όταν αναφερόμαστε σε αυτό
        {
            case "swish1":
                source.PlayOneShot(swingSound);
                break;
            case "enemyDeath1":
                source.PlayOneShot(enemy1Death);
                break;
            case "enemyDeath2":
                source.PlayOneShot(enemy2Death);
                break;
            case "enemyDeath3":
                source.PlayOneShot(enemy3Death);
                break;
            case "playerHit":
                source.PlayOneShot(playerHit);
                break;
            case "playerJump":
                source.PlayOneShot(playerJump);
                break;
        }
    }

    void Update()
    {
  
    }
}
