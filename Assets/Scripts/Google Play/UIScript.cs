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

    //public void Increment()
    //{
    //    PlayGames.IncrementAchievement(GPGSIds.achievement_incremental_achievement, 5);
    //}

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
