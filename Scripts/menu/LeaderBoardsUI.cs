using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LeaderBoardsUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lNameText = null;
    [SerializeField]
    private TextMeshProUGUI lScoreText = null;


    public void Initialise(PlayerStatsVariable playerStats)
    {
        lNameText.text = playerStats.playerName;
        lScoreText.text = playerStats.playerScore.ToString();
    }
}


