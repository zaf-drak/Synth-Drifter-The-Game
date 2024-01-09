using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class textMoveDeath : MonoBehaviour
{
    public TextMeshProUGUI TextPro;

    public float ScrollSpeed;

    private TextMeshProUGUI TextProClone;

    private RectTransform m_textRect;
    private string sourcetext;
    private string temptext;


    private float width;
    private float scrollPos;
    private Vector3 startPos;


    // κάνει κάτι όταν ξεκινάει το παιχνίδι
    void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Menu")
        {
            TextPro.text = "TWRP - Prismatic Core";
        }
        else if (sceneName == "Gameplay")
        {

        }
        else if (sceneName == "Death")
        {
            TextPro.text = "David Hasselhoff - True Survivor";
        }


        m_textRect = TextPro.GetComponent<RectTransform>();

        TextProClone = Instantiate(TextPro) as TextMeshProUGUI;
        RectTransform clone = TextProClone.GetComponent<RectTransform>();
        clone.SetParent(m_textRect);
        clone.position = new Vector3(m_textRect.position.x + (TextPro.preferredWidth / 1.5f) , m_textRect.position.y , 0);
        clone.anchorMin = new Vector2(1 , 0.5f);
        clone.localScale = new Vector3(1 , 1 , 1);
        Debug.Log("m_textRect.position = " + m_textRect.position);





    }



    // η μέθοδος Update καλείται κάθε για κάθε frame (καρέ)
    IEnumerator Start()
    {
        width = TextPro.preferredWidth;
        startPos = m_textRect.position;
        scrollPos = 0;



        while (true)
        {

            m_textRect.position = new Vector3(-scrollPos % width , startPos.y , startPos.z); // όταν το scrollPos γίνει μεγαλύτερο από το width, λόγο της διαίρεσσης κάνει reset στο 0 το αποτέλεσμα

            scrollPos += ScrollSpeed * Time.deltaTime;
            yield return null;
        }
    }
}

