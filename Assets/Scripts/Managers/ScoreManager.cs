﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int CoinValue;
    public static ScoreManager Instance;
    private GameManager gm;
    public Text ScoreText, MoneyText, ComboText, MoneyInGameText;
    public float lastScore;
    private AchievementsManager am;
    [Header("DO NOT MODIFY")]
    public float PlayerScore;
    public float PlayerMoney;
    public float MoneyInGame;
    public float distance;
    public float ComboValue = 1;
    [SerializeField]
    private float bestScore;

    //private bool doOnce1 = false;

    private int lastIntScore =  -1;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("2 ScoreManager??");
            return;
        }
        Instance = this;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        am = AchievementsManager.Instance;

        FindObjectOfType<SkinMenu>().LoadMoney();
        
        DataScript data = SaveSystem.LoadBestScore();
        bestScore = data.bestScore;

        MoneyInGame = 0;
        MoneyInGameText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        

        MoneyText.text = "" + PlayerMoney;

        MoneyInGameText.text = "" + MoneyInGame;

        if(MoneyInGame >= 9999)
        {
            MoneyInGame = 9999;
        }

        if (!gm.IsPlayerDead && gm.HasGameStarted)
        {
            MoneyInGameText.gameObject.SetActive(true);
            PlayerScore += Time.deltaTime * 2.0f * ComboValue;
            distance += Time.deltaTime * 2.0f;
            if(PlayerScore > bestScore && lastIntScore == -1)
            {
                gm.PlayerAudioSource.PlayOneShot(gm.PlayerAudioClips[6]);
                lastIntScore = 0;
                //if(Mathf.RoundToInt(PlayerScore) > lastIntScore)
                // {
                //if (lastIntScore == -1)
                //    {
                        
                //    }
                   // gm.PlayerAudioSource.PlayOneShot(gm.PlayerAudioClips[5]);
               // }
                    
               // lastIntScore = Mathf.RoundToInt(PlayerScore);

            }


            //if( (int)PlayerScore % 5 == 0 && PlayerScore != 0 && !doOnce1)
            //{
            //    UIScript.Instance.ScoreAchievement1();
            //    doOnce1 = true;
            //    Debug.Log("Score 1");
            //}

            //if ( (int)PlayerScore % 20 == 0 && PlayerScore != 0)
            //{
            //    UIScript.Instance.ScoreAchievement2();
            //    Debug.Log("Score 2");
            //}

            //if ( (int)PlayerScore % 100 == 0 && PlayerScore != 0)
            //{
            //    UIScript.Instance.ScoreAchievement3();
            //    Debug.Log("Score 3");
            //}

            bool doOnce1 = false;
            bool doOnce2 = false;
            bool doOnce3 = false;

            if(PlayerScore >= 20 && !doOnce1)
            {
                UIScript.Instance.ScoreAchievement1();
                doOnce1 = true;
                Debug.Log("Score 1");
            }

            if (PlayerScore >= 2000 && !doOnce2)
            {
                UIScript.Instance.ScoreAchievement2();
                doOnce2 = true;

            }

            if (PlayerScore >= 10000 && !doOnce3)
            {
                UIScript.Instance.ScoreAchievement3();
                doOnce3 = true;

            }


            //if(ComboValue <= 1)
            //{
            //    ScoreText.text = "" + PlayerScore.ToString("F0");
            //}
            //else
            //{ 
            //    ScoreText.text = "" + PlayerScore.ToString("F0")/* + "\n x " + ComboValue*/;
            //    ComboText.text = "x " + ComboValue;
            //}

            ScoreText.text = "" + PlayerScore.ToString("F0");
            if (ComboValue > 1)
            {
                ComboText.text = "x " + ComboValue;
            }
            else
            {
                ComboText.text = "";
            }
            
        }
        else
        {
            ScoreText.text = "";
            
            //MoneyText.text = "Money: " + PlayerMoney;

        }


    }

    public void PickupCoin()
    {
        am.AddCoin(1);
        am.AddMoney(Mathf.RoundToInt(CoinValue * ComboValue));

        PlayerScore += 20 * ComboValue;

        MoneyInGame += CoinValue * ComboValue;
       
        
        ComboValue = 1;
    }

    public void KillEnnemy()
    {
        PlayerScore += 100 * ComboValue;
    }

}
