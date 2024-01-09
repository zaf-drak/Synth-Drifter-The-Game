using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DeathScript : MonoBehaviour
{
    public FloatVariable Score;
    public FloatVariable Highscore;
    public GameObject InputPanel;
    public ScoreListVariable scoreList;
    public GameObject LeaderBoardSystem;

    private void Start()
    {
        enablePanel();
    }
    public void disablePanel()
    {
        InputPanel.gameObject.SetActive(false);
    }
    private void enablePanel()
    {
        if (scoreList.highscores.Count < 5)
        {
            InputPanel.gameObject.SetActive(true);
            LeaderBoardSystem.gameObject.SetActive(false);

        }
        else if (Score.value > scoreList.highscores[scoreList.highscores.Count - 1].playerScore)
        {
            InputPanel.gameObject.SetActive(true);
            LeaderBoardSystem.gameObject.SetActive(false);

        }
        else
        {
            InputPanel.gameObject.SetActive(false);
            LeaderBoardSystem.gameObject.SetActive(true);

        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }


    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
