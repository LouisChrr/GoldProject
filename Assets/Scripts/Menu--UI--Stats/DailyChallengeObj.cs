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

    private int coinsPickedUp;
    private int money;
    private int obstaclesDodged;
    private int deaths;
    private int levelsPassed;
    private int distance;
    private int score;
    public int Score {
        get {
            return score;

        }
        set {
            if (score >= ScoreToReach && !IsCompleted) ChallengeWon();
            score = value;
        }
    }

    public int Distance
    {
        get
        {
            return distance;

        }
        set
        {
            if (distance >= DistanceToReach && !IsCompleted) ChallengeWon();
            distance = value;
        }
    }
 

    public int CoinsPickedUp
    {
        get
        {
            return coinsPickedUp;

        }
        set
        {
            if (coinsPickedUp >= CoinsPickedUpToReach && !IsCompleted) ChallengeWon();
            coinsPickedUp = value;
        }
    }

    public int Money
    {
        get
        {
            return money;

        }
        set
        {
            if (money >= MoneyToReach && !IsCompleted) ChallengeWon();
            money = value;
        }
    }

    public int ObstaclesDodged
    {
        get
        {
            return obstaclesDodged;

        }
        set
        {
            if (obstaclesDodged >= ObstaclesDodgedToReach && !IsCompleted) ChallengeWon();
            obstaclesDodged = value;
        }
    }
    public int Deaths
    {
        get
        {
            return deaths;

        }
        set
        {
            if (deaths >= DeathsToReach && !IsCompleted) ChallengeWon();
            deaths = value;
        }
    }

    public int LevelsPassed
    {
        get
        {
            return levelsPassed;

        }
        set
        {
            if (levelsPassed >= LevelsPassedToReach && !IsCompleted) ChallengeWon();
            levelsPassed = value;
        }
    }


    public bool IsCompleted;

    public void ResetValues()
    {
        Debug.Log("ISOSUM");
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
        Debug.Log("CHALLENGE WON!!!");
    }


    [TextArea]
    public string ChallengeDescription;
    public int CoinsWon;

   
}
