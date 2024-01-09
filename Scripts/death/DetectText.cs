using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;



public class DetectText : MonoBehaviour
{
    public FloatVariable Score;
    public TMP_InputField InputField;
    public GameObject InputPanel;
    public GameObject LeaderBoardSystem;

    private string SavePath => $"{Application.persistentDataPath}/highscores.json";
    private int maxEntries = 5;
    private bool stats_flag;

    void Start()
    {
        stats_flag = false;
    }
    public void AddEntry(PlayerStatsVariable playerStatsVariable)
    {
        PlayerStatsList playerStatsList = GetSavedScores();

        bool scoreAdded = false;

        //Check if the score is high enough to be added.
        for (int i = 0; i < playerStatsList.highscores.Count; i++)
        {
            if (playerStatsVariable.playerScore > playerStatsList.highscores[i].playerScore)
            {
                playerStatsList.highscores.Insert(i , playerStatsVariable);
                scoreAdded = true;
                break;
            }
        }

        //Check if the score can be added to the end of the list.
        if (!scoreAdded && playerStatsList.highscores.Count < maxEntries)
        {
            playerStatsList.highscores.Add(playerStatsVariable);
        }



        //sort the list from highest to lowest
        for (int i = 0; i < playerStatsList.highscores.Count; i++)
        {
            for (int j = i + 1; j < playerStatsList.highscores.Count; j++)
            {
                if (playerStatsList.highscores[j].playerScore > playerStatsList.highscores[i].playerScore)
                {
                    // Swap
                    PlayerStatsVariable tmp = playerStatsList.highscores[i];
                    playerStatsList.highscores[i] = playerStatsList.highscores[j];
                    playerStatsList.highscores[j] = tmp;
                }
            }
        }
        //Remove any scores past the limit.
        if (playerStatsList.highscores.Count > maxEntries)
        {
            playerStatsList.highscores.RemoveRange(maxEntries , playerStatsList.highscores.Count - maxEntries);
        }

        SaveScores(playerStatsList);
        InputPanel.gameObject.SetActive(false);
        LeaderBoardSystem.gameObject.SetActive(true);

    }

    public PlayerStatsList GetSavedScores()
    {
        if (!File.Exists(SavePath))
        {
            File.Create(SavePath).Dispose();
            return new PlayerStatsList();
        }

        using (StreamReader stream = new StreamReader(SavePath))
        {
            string json = stream.ReadToEnd();

            return JsonUtility.FromJson<PlayerStatsList>(json);
        }
    }

    public void SaveScores(PlayerStatsList playerStatsListSaveData)
    {
        using (StreamWriter stream = new StreamWriter(SavePath))
        {
            string json = JsonUtility.ToJson(playerStatsListSaveData , true);
            stream.Write(json);
        }
    }

    public void GetPlayerstats()
    {
        PlayerStatsVariable playerStats = new PlayerStatsVariable();

        if (InputField.text != null && stats_flag == false)
        {
            playerStats.playerName = InputField.text;
            playerStats.playerScore = Score.value;
            AddEntry(playerStats);

            stats_flag = true;
           
        }
        else
        {

            Debug.Log("nothing");
        }
    }

}