using UnityEngine;

[CreateAssetMenu(menuName = "Speed Control")] //εδώ ορίζουμε τι όνομα θα έχει στο menu ως Asset το  Speed Control
public class SpeedControl : ScriptableObject //όρισμα δημόσιας κλάσης Speed Control τύπου ScriptableObject
{
    public float ForeGroundSpeed = 4; //αρχικοποίηση των ταχυτήτων που θέλουμε να χρησιμοποιούμε στα διαφορετικά παρασκήνια μας καθώς και στους διαφορετικούς τύπους εχθρών 
    public float BackGroundSpeed = 2;
    public float FarGroundSpeed = 1;
    public float enemy1 = 4;
    public float enemy2 = 6; 
    public float enemy3 = 2;

    public void reset() //δημιουργία της reset function η οποία θα καλείτε στην Start όλων των script που χρησιμοποιούν τιμές από τη SpeedControl, με σκοπό να αρχικοποιούντε κάθε φορά οι τιμές.
{
	ForeGroundSpeed = 4;
    BackGroundSpeed = 2;
    FarGroundSpeed = 1;
    enemy1 = 4;
    enemy2 = 6;
    enemy3 = 2;
}

public void bgIncrease(int x) //δημιουργία της bgIncrease function η οποία θα λαμβάνει ένα όρισμα το οποίο θα αυξάνει τις ταχύτητες των παρασκήνιων κατά το νούμερο που έχουμε ορίσει
{
	ForeGroundSpeed += x;
    BackGroundSpeed += x;
    FarGroundSpeed += x;
}

public void enemyIncrease(int x, int y) //δημιουργία της enemyIncrease function η οποία λαμβάνει δυο ορίσματα, το πρώτο είναι αυθεντικοποίησης του εχθρού που θέλουμε να αυξήσουμε την ταχύτητα του και το δεύτερο όρισμα είναι το νούμερο κατά το οποίο θα αυξήσουμε την ταχύτητα
{
	if (x == 1)
	{
		enemy1 += y;
	}
	if (x == 2)
	{
		enemy2 += y;
	}
	if (x == 3)
	{
		enemy3 += y;
	}
}

}



