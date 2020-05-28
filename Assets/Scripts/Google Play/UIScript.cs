using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    public static UIScript Instance { get; private set; }

    // Use this for initialization
    void Start()
    {
        Instance = this;
    }

    [SerializeField]

    public void GetPoint()
    {
        ManagerAchievements.Instance.IncrementCounter();
    }

    public void Restart()
    {
        ManagerAchievements.Instance.RestartGame();
    }

    public void ScoreAchievement1()
    {
        PlayGames.ScoreAchievement1(GPGSIds.achievement_amateur_ball/*, 1*/);
    }

    public void ScoreAchievement2()
    {
        PlayGames.ScoreAchievement2(GPGSIds.achievement_amateur_ball/*, 1*/);
    }

    public void ScoreAchievement3()
    {
        PlayGames.ScoreAchievement3(GPGSIds.achievement_amateur_ball/*, 1*/);
    }

    public void UnlockedBigBoss()
    {
        PlayGames.BigBossAchievement(GPGSIds.achievement_big_boss);
    }

    public void ShowAchievements()
    {
        PlayGames.ShowAchievementsUI();
        Debug.Log("Achievements");
    }

    public void ShowLeaderboards()
    {
        PlayGames.ShowLeaderboardsUI();
    }

}
