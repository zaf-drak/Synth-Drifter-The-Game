using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(menuName = "Score List Variable")]
public class ScoreListVariable : ScriptableObject
{
    public List<PlayerStatsVariable> highscores;  
}
