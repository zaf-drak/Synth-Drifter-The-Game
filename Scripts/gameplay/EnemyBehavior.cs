using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private BoxCollider2D boxCollider2d; //δημιουργία μεταβλητής για χρήση BoxCollider
                                         //public	float moveSpeed = 10f; //αρχικοποίηση τιμής ταχύτητας
    public SpeedControl SpeedControl; //όρισμα τύπου SpeedControl μεταβλητής SpeedControl
    public StringVariable lane; //όρισμα τύπου StringVariable μεταβλητής lane
    private SpriteRenderer sprite; //όρισμα τύπου SpriteRenderer μεταβλητής sprite

    public Animator animator; //ορισμα τύπου Animator
    public bool speedHitBool;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>(); //η μετάβλητή sprite παίρνει τις τιμές που αντιστοιχούν στο SpriteRenderer του αντικειμένου
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        //καλουμε τη boxCollider2d να λαβει το BoxCollider2D που έχουμε δώσει στον εχθρό μας
        animator.SetBool("Is_Hit", false);
        speedHitBool = false;
    }


    void Update()
    {
        if (this == gameObject.CompareTag("Enemy1"))
        {
            if (speedHitBool == true)
            {

                transform.Translate(Vector2.left * SpeedControl.ForeGroundSpeed * Time.deltaTime);
            }
            else
            {

                transform.Translate(Vector2.left * SpeedControl.enemy1 * Time.deltaTime);
            }
        }

        //Αν αυτό που έχει το script είναι αντικείμενο με tag "Enemy1"
        //τότε το αντικείμενο παίρνει ταχύτητα ίση με Vector2.left = αριστερά * SpeedControl.enemy1 * το χρόνο για να μπορέσει να το κάνει οσο περνάει ο χρόνος. Το ίδιο ακριβώς κάνουμε για τα επόμένα 3 είδη εχθρών

        else if (this == gameObject.CompareTag("Enemy2"))
        {

            if (speedHitBool == true)
            {
                transform.Translate(Vector2.left * SpeedControl.ForeGroundSpeed * Time.deltaTime);
            }
            else
            {

                transform.Translate(Vector2.left * SpeedControl.enemy2 * Time.deltaTime);
            }
        }
        else if (this == gameObject.CompareTag("Enemy3"))
        {
            if (speedHitBool == true)
            {
                transform.Translate(Vector2.left * SpeedControl.ForeGroundSpeed * Time.deltaTime);
            }
            else
            {

                transform.Translate(Vector2.left * SpeedControl.enemy3 * Time.deltaTime);
            }
        }
        else if (this == gameObject.CompareTag("Enemy4"))
        {
            transform.Translate(Vector2.left * SpeedControl.enemy1 * Time.deltaTime);
        }
        //ο λόγος που βάζουμε στον Enemy4 ταχύτητα ίση με SpeedControl.enemy1 είναι καθαρά διότι έχουμε αποφασίσει από πριν ότι θέλουμε το 1 και το 4 να έχουν την ίδια ταχύτητα, οπότε δεν χρειάζεται να αλλάξουμε κάτι
        if (transform.position.x <= -15)
        // αν η θεση του εχθρού φτάσει το -10 στον άξονα x τότε
        {
            Destroy(gameObject); //ο εχθρός αυτοκαταστρέφετε
        }
    }


    void OnTriggerStay2D(Collider2D other)
    { //όταν το collider του αντικειμένου αυτού
        if (other.gameObject.CompareTag("Player") && this.gameObject.layer == LayerMask.NameToLayer(lane.txt))
        { //έρθει σε επαφή με ένα αντικείμενο το οποίο έχει το Tag (χαρακτηρισμό) Player, τότε

            animator.SetBool("Is_Hit", true);
            speedHitBool = true;

            if (this == gameObject.CompareTag("Enemy1"))
            {
                SoundManager.PlaySound("enemyDeath1");

            }
            else if (this == gameObject.CompareTag("Enemy2"))
            {
                SoundManager.PlaySound("enemyDeath2");

            }

            else if (this == gameObject.CompareTag("Enemy3"))
            {
                SoundManager.PlaySound("enemyDeath3");
            }
            Destroy(this.boxCollider2d); //ο εχθρός αυτοκαταστρέφετε.
        }
       

    }
}
