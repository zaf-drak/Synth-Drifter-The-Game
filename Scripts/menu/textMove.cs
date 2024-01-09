using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class textMove : MonoBehaviour
{
    public TextMeshProUGUI TextPro;

    public float ScrollSpeed;

    private TextMeshProUGUI TextProClone;

    private RectTransform m_textRect;
    private string sourcetext;
    private string temptext;


    //public float speed;
    private float width;
    private float scrollPos;
    private Vector3 startPos;

    public StringVariable songName;

    // κάνει κάτι όταν ξεκινάει το παιχνίδι
    void Start()
    {

        setName();

        m_textRect = TextPro.GetComponent<RectTransform>();
        width = TextPro.preferredWidth;
        startPos = m_textRect.position;
        scrollPos = 0;

    }

    void setName()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Menu")
        {
            songName.txt = "TWRP - Prismatic Core";
        }
        else if (sceneName == "Gameplay")
        {
            //do nothing
        }
        else if (sceneName == "Death")
        {
            songName.txt = "David Hasselhoff - True Survivor";
        }
    }

    void Update()
    {
        TextPro.text = songName.txt;

        m_textRect.transform.Translate(Vector2.left * ScrollSpeed * (Time.deltaTime % 1.0f));

        scrollPos += ScrollSpeed * Time.deltaTime;

        if (scrollPos >= m_textRect.position.x + width * 2.5f)
        {
            m_textRect.position = startPos;
            scrollPos = 0;
        }

    }
}
