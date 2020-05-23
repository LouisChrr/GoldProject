using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int CoinValue;
    public static ScoreManager Instance;
    private GameManager gm;
    public Text ScoreText, MoneyText, ComboText;
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
    }

    // Update is called once per frame
    void Update()
    {
        

        MoneyText.text = "" + PlayerMoney;
     

        if (!gm.IsPlayerDead && gm.HasGameStarted)
        {
            PlayerScore += Time.deltaTime * 2.0f * ComboValue;
            distance += Time.deltaTime * 2.0f;
            if(PlayerScore > bestScore)
            {
                if(Mathf.RoundToInt(PlayerScore) > lastIntScore)
                {
                    if(lastIntScore == -1)
                    {
                        gm.PlayerAudioSource.PlayOneShot(gm.PlayerAudioClips[5]);
                    }
                    gm.PlayerAudioSource.PlayOneShot(gm.PlayerAudioClips[6]);
                }
                    
                lastIntScore = Mathf.RoundToInt(PlayerScore);

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
        PlayerMoney += MoneyInGame;
        
        ComboValue = 1;
    }

    public void KillEnnemy()
    {
        PlayerScore += 100 * ComboValue;
    }

}
