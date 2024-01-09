using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarting : MonoBehaviour
{


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //αν πατηθεί το κουμπί Space τότε
        {
            SceneManager.LoadScene("Gameplay"); //φόρτωσε τη σκηνή gameplay
        }

        if (Input.GetKeyDown(KeyCode.Escape)) //αν πατηθεί το κουμπί Escape τότε
        {
            Application.Quit(); //κλείσε την εφαρμογή
        }
    }
}
