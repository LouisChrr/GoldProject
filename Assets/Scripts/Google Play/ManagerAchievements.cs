using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAchievements : MonoBehaviour
{
    public static ManagerAchievements Instance { get; private set; }
    public static int Counter { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementCounter()
    {
        Counter++;
    }

    public void RestartGame()
    {
        PlayGames.AddScoreToLeaderBoard(GPGSIds.leaderboard_leaderboard, Counter);
        Counter = 0;
    }
}
