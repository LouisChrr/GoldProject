using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsManager : MonoBehaviour
{
    public LifetimeStatsObject LifetimeStatsObj;
    public static AchievementsManager Instance;
    [SerializeField]
    private List<DailyChallengeObj> DailyChallenges = new List<DailyChallengeObj>();
    public List<DailyChallengeObj> ActiveChallenges;
    public int lastDate;

    public Text[] descText = new Text[3];
    public Text[] challengeMoney = new Text[3];
    public GameObject[] completedText = new GameObject[3];

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null) Debug.LogError("wtf 2 achievemtns manager");
        else Instance = this;

        if (System.DateTime.Now.Minute != PlayerPrefs.GetInt("lastDate", 0))
        {
            PlayerPrefs.SetInt("lastDate", System.DateTime.Now.Minute);
            ActiveChallenges.Clear();
            ActiveChallenges = GetNewDailyChallenges();
            Debug.Log("On refresh les challenges!");
        }
        else
        {
            foreach (DailyChallengeObj challenge in DailyChallenges)
            {
                if (challenge.IsActive)
                {
                    ActiveChallenges.Add(challenge);

                }
            }
   
        }
    }

    private void Start()
    {
        // Debug.Log(System.DateTime.Now.Day);
        //for(int i = 0; i < completedText.Length; i++)
        //{
        //    completedText[i].SetActive(false);
        //}

        for (int i = 0; i < AchievementsManager.Instance.ActiveChallenges.Count; i++)
        {

            descText[i].text = AchievementsManager.Instance.ActiveChallenges[i].ChallengeDescription;
            challengeMoney[i].text = "" + AchievementsManager.Instance.ActiveChallenges[i].CoinsWon;

            if (AchievementsManager.Instance.ActiveChallenges[i].IsCompleted)
            {
                challengeMoney[i].gameObject.SetActive(false);
                completedText[i].SetActive(true);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < AchievementsManager.Instance.ActiveChallenges.Count; i++)
        //{

        //    descText[i].text = AchievementsManager.Instance.ActiveChallenges[i].ChallengeDescription;
        //    challengeMoney[i].text = "" + AchievementsManager.Instance.ActiveChallenges[i].CoinsWon;

        //    if (AchievementsManager.Instance.ActiveChallenges[i].IsCompleted)
        //    {
        //        challengeMoney[i].gameObject.SetActive(false);
        //        completedText[i].SetActive(true);
        //    }
        //}
    }



    private List<DailyChallengeObj> GetNewDailyChallenges()
    {
        foreach (DailyChallengeObj challenge in DailyChallenges)
        {
             challenge.ResetValues();
        }

        List<DailyChallengeObj> newList = new List<DailyChallengeObj>();
        int random = 0;

        for(int i = 0; i < 3; i++)
        {
            random = Random.Range(0, DailyChallenges.Count);

            while (newList.Contains(DailyChallenges[random]))
            {
                random = Random.Range(0, DailyChallenges.Count);
            }
            newList.Add(DailyChallenges[random]);
            newList[i].ResetValues();
            newList[i].IsActive = true;
        }
        return newList;
    }

    public void AddScore(int score)
    {
        LifetimeStatsObj.LifetimeScore += score;
        foreach(DailyChallengeObj challenge in ActiveChallenges)
        {
            challenge.Score += score;
        }
    }

    public void AddDistance(int distance)
    {
        foreach (DailyChallengeObj challenge in ActiveChallenges)
        {
            challenge.Distance += distance;
        }
        LifetimeStatsObj.LifetimeDistance += distance;
    }

    public void AddCoin(int coin)
    {
        LifetimeStatsObj.LifetimeCoinsPickedUp += coin;
        foreach (DailyChallengeObj challenge in ActiveChallenges)
        {
            challenge.CoinsPickedUp += coin;
        }
    }

    public void AddMoney(int money)
    {
        LifetimeStatsObj.LifetimeMoney += money;
        foreach (DailyChallengeObj challenge in ActiveChallenges)
        {
            challenge.Money += money;
        }
    }

    public void AddObstacleDodged(int number)
    {
        LifetimeStatsObj.LifetimeObstaclesDodged += number;
        foreach (DailyChallengeObj challenge in ActiveChallenges)
        {
            challenge.ObstaclesDodged += number;
        }
    }

    public void AddDeath(int number) 
    {
        LifetimeStatsObj.LifetimeDeaths += number;
        foreach (DailyChallengeObj challenge in ActiveChallenges)
        {
            challenge.Deaths += number;
        }
    }

    public void AddLevelPassed(int number)
    {
        if (number <= 0) return;
        LifetimeStatsObj.LifetimeLevelsPassed += number;
        foreach (DailyChallengeObj challenge in ActiveChallenges)
        {
            challenge.LevelsPassed += number;
        }
    }

    public void UpdateChallenges()
    {

        for (int i = 0; i < AchievementsManager.Instance.ActiveChallenges.Count; i++)
        {

            //descText[i].text = AchievementsManager.Instance.ActiveChallenges[i].ChallengeDescription;
            //challengeMoney[i].text = "" + AchievementsManager.Instance.ActiveChallenges[i].CoinsWon;

            if (AchievementsManager.Instance.ActiveChallenges[i].IsCompleted)
            {
                challengeMoney[i].gameObject.SetActive(false);
                completedText[i].SetActive(true);
            }

        }

    }

}
