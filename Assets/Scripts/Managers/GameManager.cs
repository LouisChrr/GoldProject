﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject LevelUpText;
    public bool HasGameStarted;
    public int LevelNb;
    public float LevelSpeed;
    public static GameManager Instance;
    public Transform Circles;
    public GameObject PlayerObj;
    public int ObstacleHP;
    public bool IsPlayerDead;
    public float InitialCircleSpeed;
    [SerializeField]
    private int circlesNumber = 0;
    private ScoreManager sm;
    public int CirclesNumber { get => circlesNumber; }
    private AchievementsManager am;

   
    public CircleGenerator generator;
    public GameObject touchFeedback; // TEMPO, A DELETE

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("2 GameManager??");
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        NewLevel(0);
        sm = ScoreManager.Instance;
        am = AchievementsManager.Instance;
    }



    public void StartGame()
    {
        IsPlayerDead = false;
        HasGameStarted = true;
        //foreach (GameObject go in generator.Circles)
        //{
        //    go.GetComponent<Circle>().ChangeBonusSpeed(0);
        //}
        NewLevel(1);
    }

    public void NewLevel(int levelNb) // A APPELER A CHAQUE NEW LEVEL ISSOUm
    {

        if (levelNb > 1)
        {
            am.AddLevelPassed(levelNb - 1);
            LevelUpText.SetActive(true);
            LevelUpText.GetComponent<Animator>().Play("TextFade", 0, 0);
        }

        ScoreManager.Instance.ComboValue = 1;

        LevelNb = levelNb;
        LevelSpeed = levelNb*0.4f +2;

        foreach (GameObject go in generator.Circles)
        {
            go.GetComponent<Circle>().ChangeBonusSpeed(LevelSpeed);
        }
    }

    public void Death()
    {
        am.AddScore(Mathf.RoundToInt(sm.PlayerScore));
        am.AddDistance(Mathf.RoundToInt(sm.distance));
        
        am.AddDeath(1);

        IsPlayerDead = true;
        MenuManager.Instance.EndGame();
        
        foreach (GameObject go in generator.Circles)
        {
            go.GetComponent<Circle>().ChangeBonusSpeed(0);
        }

        touchFeedback.SetActive(false);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}