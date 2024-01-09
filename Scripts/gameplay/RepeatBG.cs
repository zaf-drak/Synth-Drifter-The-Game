using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SynthDrifter;

namespace SynthDrifter
{
    // Η κλάση αυτή φροντίζει για την κύλιση του παρασκηνίου
    public class RepeatBG : MonoBehaviour
    {
        // μεταβλητές που φαίνονται και "απέξω"	  
        public float speed;
        public SpeedControl SpeedControl;
        // κρυφές μεταβλητές μόνο προσβάσιμες από τη κλάση
        private float length, startX;

        // κάνει κάτι όταν ξεκινάει το παιχνίδι
        void Start()
        {
            // θέτει στην startX το από που ξεκινάει η εικόνα μας απο τα αριστερά
            startX = transform.position.x;
            // βρίσκει και θέτει το μήκος της εικόνας βρίσκοντας το τέλος της εικόνας
            length = GetComponent<SpriteRenderer>().bounds.size.x;
            // υπολογίζει την δεξιά άκρη της εικόνας
        }


        // η μέθοδος Update καλείται κάθε για κάθε frame (καρέ)
        public void FixedUpdate()
        {
            if (this == gameObject.CompareTag("ForeGround"))
            {
                speed = SpeedControl.ForeGroundSpeed;
            }
            else if (this == gameObject.CompareTag("BackGround"))
            {
                speed = SpeedControl.BackGroundSpeed;
            }
            else if (this == gameObject.CompareTag("FarGround"))
            {
                speed = SpeedControl.FarGroundSpeed;
            }

            // κινεί την εικόνα προς τα αριστερά με την ταχύτητα που ορίσαμε και ανα καρέ,
            //με το % 1.0f καταφέρνουμε να στρογγυλοποιήσουμε  το Time.deltaTime για να καταφέρουμε να δώσουμε μια πιο εμφανίσιμη εικόνα στο χρήστη Αν δοκιμάσετε να το τρέξετε χωρίς το % 1.0f θα δείτε ότι το παιχνίδι μας κάνει stuttering το οποίο σημαίνει ότι φαίνεται σαν να τρεμοπαίζει ενώ κινείτε το οποίο δίνει ένα αίσθημα κούρασης.
            transform.Translate(Vector2.left * speed * (Time.deltaTime % 1.0f));


            if (transform.position.x <= -length)  //όταν φτάσει στο τέλος της κάμερας
            {
                // ορίζει την θέση του από την αρχή για να ξαναγίνει κύλιση επ αόριστον
                Vector2 pos = new Vector2(length * 2f , 0); // το pos είναι μια μεταβλητή με 2 ορίσματα (x,y), οπότε φτιαχνουμε μια μεταβλητή που είναι θέση να είναι ίση με το διπλάσιο του πλάτους της έτσι ώστε να δίνει την αίσθηση της κύλισης
                transform.position = (Vector2)transform.position + pos; //οποτε χρησιμοποιωντας το pos απο πριν δινουμε εντολή στο προγραμμα να πάρει νεα θεση ηση με αυτη που εχει τωρα + δυο φορες το μεγεθος της σε μηκος ετσι ωστε να πάρει τη νεα θεση
            }
        }
    }
}