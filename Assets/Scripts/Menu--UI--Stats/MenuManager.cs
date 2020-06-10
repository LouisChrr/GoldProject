using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public GameObject _panelPause;

    public GameObject imgPause;
    public GameObject imgPlay;

    public GameObject menu;
    public GameObject options;
    public GameObject pause;
    public GameObject end;
    public GameObject tuto;

    public GameObject backToMenu;
    public GameObject backToPlay;

    public GameObject greyPanel;

    public Countdown countdown;
    public int countDownTime = 3;

    public AudioSource MusicAudioSource;

    public AchievementsManager achievementsManager;

    public Text ScoreText, MoneyText, BestScoreText, MenuMoneyText;
    public float PlayerBestScore = 0;

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

        imgPause.SetActive(false);
        imgPlay.SetActive(false);

        menu.SetActive(true);
        options.SetActive(false);
        pause.SetActive(false);
        end.SetActive(false);
        tuto.SetActive(false);

        greyPanel.SetActive(true);

        MenuMoneyText.text = "" + FindObjectOfType<ScoreManager>().PlayerMoney;

        LoadBestScore();
        
        backToMenu.SetActive(true);
        backToPlay.SetActive(false);
        


    }

    private void Update()
    {
        
    }

    public void EndGame()
    {
        end.SetActive(true);
        ScoreText.text = "" + (int)ScoreManager.Instance.PlayerScore;
        MoneyText.text = "+ " + ScoreManager.Instance.MoneyInGame;

        if (ScoreManager.Instance.PlayerScore > PlayerBestScore){
            //PlayerPrefs.SetFloat("BestScore", ScoreManager.Instance.PlayerScore);
            PlayerBestScore = ScoreManager.Instance.PlayerScore;
            SaveBestScore();
            PlayGames.AddScoreToLeaderBoard(GPGSIds.leaderboard_leaderboard, Convert.ToInt64(PlayerBestScore));
        }
        BestScoreText.text = "" + (int)PlayerBestScore;
        
    }

    public void MyLoadScene(string nameScene)
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(nameScene);
    }

    public void Options()
    {
        options.SetActive(true);

        if (FindObjectOfType<GameManager>().HasGameStarted)
        {
            pause.SetActive(false);
            greyPanel.SetActive(true);
        }
        else
        {
            menu.SetActive(false);
        }

    }

    public void BackToPause()
    {
        options.SetActive(false);
        greyPanel.SetActive(false);
        pause.SetActive(true);
    }

    public void Menu()
    {
        menu.SetActive(true);
        options.SetActive(false);
        end.SetActive(false);
        tuto.SetActive(false);
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
        pause.SetActive(true);
        imgPause.SetActive(true);
    }

    private void Pause(bool pause)
    {
        _isPause = pause;

        if (pause)
        {
            Time.timeScale = 0;

            imgPause.SetActive(false);
            imgPlay.SetActive(true);

            backToMenu.SetActive(false);
            backToPlay.SetActive(true);

            MusicAudioSource.Pause();

            achievementsManager.UpdateChallenges();

            Debug.Log("On Pause");
        }
        else
        {
            Time.timeScale = 1;

            imgPause.SetActive(true);
            imgPlay.SetActive(false);

            Debug.Log("On Play");
        }

        _panelPause.SetActive(_isPause);
    }

    public void SaveBestScore()
    {
        SaveSystem.SaveBestScore(this);
    }

    public void LoadBestScore()
    {
        DataScript data = SaveSystem.LoadBestScore();

        PlayerBestScore = data.bestScore;

    }
    
    public void Countdown()
    {

        StartCoroutine(CountDown());

    }

    public void Tuto()
    {
        tuto.SetActive(true);
        menu.SetActive(false);
    }

    IEnumerator CountDown()
    {

        imgPause.GetComponent<Button>().interactable = false;
        //FindObjectOfType<GameManager>().HasGameStarted = false;
        Time.timeScale = 0;
        countdown.gameObject.SetActive(true);
        countdown.countdownTime = countDownTime;

        while (countdown.countdownTime > 0)
        {
            countdown.GetComponent<Text>().text = countdown.countdownTime.ToString();

            yield return new WaitForSecondsRealtime(1f);

            countdown.countdownTime--;
        }

        countdown.gameObject.SetActive(false);
        Time.timeScale = 1;
        //FindObjectOfType<GameManager>().HasGameStarted = true;
        MusicAudioSource.UnPause();
        imgPause.GetComponent<Button>().interactable = true;

    }

    public void UnlockAchievement()
    {
        UIScript.Instance.UnlockedBigBoss();
    }
}
