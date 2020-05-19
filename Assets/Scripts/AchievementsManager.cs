using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    public LifetimeStatsObject LifetimeStatsObj;
    public static AchievementsManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null) Debug.LogError("wtf 2 achievemtns manager");
        else Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int score)
    {
        LifetimeStatsObj.LifetimeScore += score;
    }

    public void AddDistance(int distance)
    {
        LifetimeStatsObj.LifetimeDistance += distance;
    }

    public void AddCoin(int coin)
    {
        LifetimeStatsObj.LifetimeCoinsPickedUp += coin;
    }

    public void AddMoney(int money)
    {
        LifetimeStatsObj.LifetimeMoney += money;
    }

    public void AddObstacleDodged(int number)
    {
        LifetimeStatsObj.LifetimeObstaclesDodged += number;
    }

    public void AddDeath(int number) 
    {
        LifetimeStatsObj.LifetimeDeaths += number;
    }

    public void AddLevelPassed(int number)
    {
        if (number <= 0) return;
        LifetimeStatsObj.LifetimeLevelsPassed += number;
    }

}
