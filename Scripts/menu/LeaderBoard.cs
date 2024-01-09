using System.IO;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField]
    private Transform leaderBoardCanvas = null;
    [SerializeField]
    private GameObject entryObject = null;

    private string SavePath => $"{Application.persistentDataPath}/highscores.json";



    private void Start()
    {

        PlayerStatsList playerStatsList = GetSavedScores();
        UpdateUI(playerStatsList);
        SaveScores(playerStatsList);
    }


    public void UpdateUI(PlayerStatsList playerStatsList)
    {
        foreach (Transform child in leaderBoardCanvas)
        {
            Destroy(child.gameObject);
        }

        foreach (PlayerStatsVariable highscore in playerStatsList.highscores)
        {
            Instantiate(entryObject , leaderBoardCanvas).GetComponent<LeaderBoardsUI>().Initialise(highscore);
        }
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
}

