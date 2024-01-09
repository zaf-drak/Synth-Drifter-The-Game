using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    public StringVariable songName;

    public static AudioClip Timecop, Magonia, Turboslash, Wice, StreetCleaner, Shredder, Occams, LazerHawk; //δηλώσεις ολων των πιθανών μετάβλητών που θα χρειαστούμε για διαφορετικούς ήχους

    static AudioSource source; //δήλωση AudioSource τύπου μετάβλητή 
    private AudioClip[] jukebox = new AudioClip[8];

    private int selectRand;


    void Awake()
    {
        source = GetComponent<AudioSource>(); //εντολή για να πάρει το Object που χρησιμοποιεί αυτό το Script το AudioSource και να το χρησιμοποιήσει

        selectRand = Random.Range(0,7);

        selectSong();        

        songName.txt = jukebox[selectRand].name.ToString();
        PlaySound();
    }

    public void selectSong()
    {
        switch (selectRand)
        {
            case 0:
            Magonia = Resources.Load<AudioClip>("We Are Magonia - The Living Will Envy the Dead");
            jukebox[0] = Magonia;
            break;

            case 1:
            Timecop = Resources.Load<AudioClip>("Timecop1983 - On the Run");
            jukebox[1] = Timecop;
            break;

            case 2:
            Turboslash = Resources.Load<AudioClip>("Turboslash - Deathracer");
            jukebox[2] = Turboslash;
            break;

            case 3:
            Wice = Resources.Load<AudioClip>("Wice - Aliens");
            jukebox[3] = Wice;
            break;

            case 4:
            StreetCleaner = Resources.Load<AudioClip>("Street Cleaner - Viper Force");
            jukebox[4] = StreetCleaner;
            break;

            case 5:
            Shredder = Resources.Load<AudioClip>("Shredder 1984 - Undead Thrasher");
            jukebox[5] = Shredder;
            break;

            case 6:
            Occams = Resources.Load<AudioClip>("Occams Laser - Temperance");
            jukebox[6] = Occams;
            break;

            case 7:
            LazerHawk = Resources.Load<AudioClip>("LazerHawk - King of The Streets");
            jukebox[7] = LazerHawk;
            break;
        }

        songName.txt = jukebox[selectRand].name.ToString();
        
    }
    public void PlaySound() //Function για να παίξει τον ήχο όταν το ζητήσουμε
    {
        source.PlayOneShot(jukebox[selectRand]);
    }
    private void nextSong()
    {

        if (Input.GetKey(KeyCode.N))
        {
            source.Stop();

            if (selectRand >= 7)
            {
                selectRand = 0;
                selectSong();
                PlaySound();
            }
            else
            {
                selectRand++;
                selectSong();
                PlaySound();
            }
        }
    }
    private void Update()
    {
        nextSong();
    }
}
