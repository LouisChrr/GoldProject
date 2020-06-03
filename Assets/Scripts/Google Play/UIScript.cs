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
    
    #region Achievements

    #region Unlock

    public void UnlockedBigBoss()
    {
        PlayGames.BigBossAchievement(GPGSIds.achievement_big_boss);
    }

    public void ScoreAchievement1()
    {
        PlayGames.ScoreAchievement1(GPGSIds.achievement_amateur_ball);
    }

    public void ScoreAchievement2()
    {
        PlayGames.ScoreAchievement2(GPGSIds.achievement_amateur_ball);
    }

    public void ScoreAchievement3()
    {
        PlayGames.ScoreAchievement3(GPGSIds.achievement_amateur_ball);
    }

    public void MoneyAchievement1()
    {
        PlayGames.MoneyAchievement1(GPGSIds.achievement_unemployed);
    }

    public void MoneyAchievement2()
    {
        PlayGames.MoneyAchievement2(GPGSIds.achievement_investor);
    }

    public void MoneyAchievement3()
    {
        PlayGames.MoneyAchievement3(GPGSIds.achievement_rich_guy);
    }

    #endregion /Unlock

    #region Increment

    public void DieAchievementIncrement1()
    {
        PlayGames.DieAchievement1(GPGSIds.achievement_unemployed, 1);
    }

    public void DieAchievementIncrement2()
    {
        PlayGames.DieAchievement2(GPGSIds.achievement_investor, 1);
    }

    public void DieAchievementIncrement3()
    {
        PlayGames.DieAchievement3(GPGSIds.achievement_rich_guy, 1);
    }

    public void DestroyAchievementIncrement1()
    {
        PlayGames.DestroyAchievement1(GPGSIds.achievement_unemployed, 1);
    }

    public void DestroyAchievementIncrement2()
    {
        PlayGames.DestroyAchievement2(GPGSIds.achievement_investor, 1);
    }

    public void DestroyAchievementIncrement3()
    {
        PlayGames.DestroyAchievement3(GPGSIds.achievement_rich_guy, 1);
    }

    public void SkinAchievementIncrement1()
    {
        PlayGames.SkinAchievement1(GPGSIds.achievement_unemployed, 1);
    }

    public void SkinAchievementIncrement2()
    {
        PlayGames.SkinAchievement2(GPGSIds.achievement_investor, 1);
    }

    public void SkinAchievementIncrement3()
    {
        PlayGames.SkinAchievement3(GPGSIds.achievement_rich_guy, 1);
    }

    #endregion /Increment

    #endregion /Achievements

    #region UI

    public void ShowAchievements()
    {
        PlayGames.ShowAchievementsUI();
    }

    public void ShowLeaderboards()
    {
        PlayGames.ShowLeaderboardsUI();
    }

    #endregion /UI

}
