﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public GameObject _panelPause;

    public GameObject imgPause;
    public GameObject imgPlay;

    public GameObject menu;
    public GameObject options;
    public GameObject game;
    public GameObject end;

    public GameObject greyPanel;

    public AudioSource audio1;
    public Text ScoreText, MoneyText, BestScoreText, MenuMoneyText;
    private bool _isPause;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("2 MenuManager??");
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 1;

        _panelPause.SetActive(false);

        imgPause.SetActive(true);
        imgPlay.SetActive(false);

        menu.SetActive(true);
        options.SetActive(false);
        game.SetActive(false);
        end.SetActive(false);

        greyPanel.SetActive(true);

        MenuMoneyText.text = "Money: " + PlayerPrefs.GetFloat("Money", 0);

    }

    public void EndGame()
    {
        end.SetActive(true);
        ScoreText.text = "Score: " + ScoreManager.Instance.PlayerScore.ToString("F0");
        MoneyText.text = "Money: " + PlayerPrefs.GetFloat("Money", 0);
        if (ScoreManager.Instance.PlayerScore > PlayerPrefs.GetFloat("BestScore", 0)){
            PlayerPrefs.SetFloat("BestScore", ScoreManager.Instance.PlayerScore);
        }
        BestScoreText.text = "Best Score: " + PlayerPrefs.GetFloat("BestScore", 0).ToString("F0");


    }

    public void MyLoadScene(string nameScene)
    {
        //Debug.Log(nameScene);
        UnityEngine.SceneManagement.SceneManager.LoadScene(nameScene);
    }

    public void Options()
    {
        options.SetActive(true);
        menu.SetActive(false);

    }

    public void Menu()
    {
        menu.SetActive(true);
        options.SetActive(false);
        end.SetActive(false);
    }

    public void DoPause()
    {
        Pause(!_isPause);
    }

    public void DoPlay()
    {
        GameManager.Instance.HasGameStarted = true;
        GameManager.Instance.StartGame();
  
       menu.SetActive(false);
        greyPanel.SetActive(false);
        game.SetActive(true);
    }

    private void Pause(bool pause)
    {
        _isPause = pause;

        if (pause)
        {
            Time.timeScale = 0;
            imgPause.SetActive(false);
            imgPlay.SetActive(true);

            audio1.Pause();

            Debug.Log("On Pause");
        }
        else
        {
            Time.timeScale = 1;
            imgPause.SetActive(true);
            imgPlay.SetActive(false);

            audio1.UnPause();

            Debug.Log("On Play");
        }

        _panelPause.SetActive(_isPause);
    }
}
