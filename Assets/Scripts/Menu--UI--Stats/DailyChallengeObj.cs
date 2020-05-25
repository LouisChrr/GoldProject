using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


[CreateAssetMenu(fileName = "DailyChallengeObj", menuName = "DailyChallengeObj", order = 1)]
public class DailyChallengeObj : ScriptableObject
{
    // nom du defi = nom de l'objet
    public int ScoreToReach;
    public int DistanceToReach;
    public int CoinsPickedUpToReach;
    public int MoneyToReach;
    public int ObstaclesDodgedToReach;
    public int DeathsToReach;
    public int LevelsPassedToReach;
  

    public int Score
    {
        get
        {
            Debug.Log("get mamaw score");
            return Score;
        }
        set
        {
            if (Score >= ScoreToReach && !IsCompleted) ChallengeWon();
            Score = value;
            Debug.Log("set mamaw score");

        }
    }



    public int Distance;
    public int CoinsPickedUp;
    public int Money;
    public int ObstaclesDodged;
    public int Deaths;
    public int LevelsPassed;

    public void ResetValues()
    {
        Score = 0;
        Distance = 0;
        CoinsPickedUp = 0;
        Money = 0;
        ObstaclesDodged = 0;
        Deaths = 0;
        LevelsPassed = 0;
        IsCompleted = false;
    }
    public void ChallengeWon()
    {
        IsCompleted = true;
        ScoreManager.Instance.PlayerMoney += CoinsWon;
    }


    [TextArea]
    public string ChallengeDescription;
    public int CoinsWon;
    public bool IsCompleted;

}
