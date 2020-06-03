using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class PlayGames : MonoBehaviour
{

    public static PlayGamesPlatform platform;

    public GameObject connected;

    // Start is called before the first frame update
    void Start()
    {
        connected.SetActive(false);
        if(platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;

            platform = PlayGamesPlatform.Activate();
            
        }

        

        SignIn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SignIn()
    {
        Social.Active.localUser.Authenticate(success => {

            if (success)
            {
                Debug.Log("Logged in successfully");
                connected.SetActive(true);
            }
            else
            {
                Debug.Log("Failed to login");
            }

        });
    }

    public void LogOut()
    {
        PlayGamesPlatform.Instance.SignOut();
    }

    #region Achievements

    #region Unlock

    public static void BigBossAchievement(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }

    public static void ScoreAchievement1(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }

    public static void ScoreAchievement2(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }

    public static void ScoreAchievement3(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }

    public static void MoneyAchievement1(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }

    public static void MoneyAchievement2(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }

    public static void MoneyAchievement3(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }

    #endregion /Unlock

    #region Increment

    public static void DieAchievement1(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }

    public static void DieAchievement2(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }

    public static void DieAchievement3(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }

    public static void DestroyAchievement1(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }

    public static void DestroyAchievement2(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }

    public static void DestroyAchievement3(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }

    public static void SkinAchievement1(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }

    public static void SkinAchievement2(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }

    public static void SkinAchievement3(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }

    #endregion /Increment

    public static void ShowAchievementsUI()
    {
        Social.ShowAchievementsUI();
    }

    #endregion /Achievements

    #region LeaderBoard

     public static void AddScoreToLeaderBoard(string leaderboardID, long score)
    {
        Social.ReportScore(score, leaderboardID, success => { });
    }

    public static void ShowLeaderboardsUI()
    {
        Social.ShowLeaderboardUI();
    }

    #endregion /LeaderBoard

}
