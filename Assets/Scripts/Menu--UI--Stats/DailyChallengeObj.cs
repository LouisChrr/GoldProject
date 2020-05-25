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

    [SerializeField]
    private int coinsPickedUp;
    [SerializeField]
    private int money;
    [SerializeField]
    private int obstaclesDodged;
    [SerializeField]
    private int deaths;
    [SerializeField]
    private int levelsPassed;
    [SerializeField]
    private int distance;
    [SerializeField]
    private int score;
    public int Score {
        get {
            return score;

        }
        set {
            if (value >= ScoreToReach && !IsCompleted) ChallengeWon();
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
            if (value >= DistanceToReach && !IsCompleted) ChallengeWon();
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
            if (value >= CoinsPickedUpToReach && !IsCompleted) ChallengeWon();
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
            if (value >= MoneyToReach && !IsCompleted) ChallengeWon();
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
            if (value >= ObstaclesDodgedToReach && !IsCompleted) ChallengeWon();
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
            if (value >= DeathsToReach && !IsCompleted) ChallengeWon();
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
            if (value >= LevelsPassedToReach && !IsCompleted) ChallengeWon();
            levelsPassed = value;
        }
    }


    

    public void ResetValues()
    {

        IsCompleted = false;

        Score = 0;
        Distance = 0;
        CoinsPickedUp = 0;
        Money = 0;
        ObstaclesDodged = 0;
        Deaths = 0;
        LevelsPassed = 0;
        
        IsActive = false;
    }
    public void ChallengeWon()
    {
        IsCompleted = true;
        ScoreManager.Instance.PlayerMoney += CoinsWon;
        Debug.Log("CHALLENGE WON!!!");
        Debug.Log(name);
    }

    public bool IsCompleted;
    public bool IsActive;
    [TextArea]
    public string ChallengeDescription;
    public int CoinsWon;

   
}
