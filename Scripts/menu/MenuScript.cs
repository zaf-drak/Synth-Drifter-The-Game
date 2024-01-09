using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class MenuScript : MonoBehaviour
{
    public ScoreListVariable scoreList;
    public GameObject leaderboards;
    public FloatVariable HighScore;
    private string SavePath ;
    private string FilePath ;
    private string jsonPath ;

    public void Start()
    {
        SavePath = $"{Application.persistentDataPath}/highscores.json";
        FilePath = $"{Application.dataPath}/Files/highscores.json";
        jsonPath = Path.Combine(Application.streamingAssetsPath, "highscores.json");

        PlayerStatsList playerStatsList = GetSavedScores();
        SaveScores(playerStatsList);
        
        scoreList.highscores = playerStatsList.highscores;
        
    }

    public PlayerStatsList GetSavedScores()
    {
        if (!File.Exists(SavePath))
        {
            using (StreamReader stream = new StreamReader(jsonPath))
            {
                string json = stream.ReadToEnd();
                return JsonUtility.FromJson<PlayerStatsList>(json);
            }           
        }
        else
        {
            using (StreamReader stream = new StreamReader(SavePath))
            {
                string json = stream.ReadToEnd();
                return JsonUtility.FromJson<PlayerStatsList>(json);
            }
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

    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
