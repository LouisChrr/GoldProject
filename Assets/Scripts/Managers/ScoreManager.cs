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
  
    [Header("DO NOT MODIFY")]
    public float PlayerScore;
    public float PlayerMoney;
    
    public float ComboValue = 1;
    
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
        
        FindObjectOfType<SkinMenu>().LoadMoney();

    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = "Money: " + PlayerMoney;
        ComboText.text = "x" + ComboValue;

        if (!gm.IsPlayerDead && gm.HasGameStarted)
        {
            PlayerScore += Time.deltaTime * 2.0f * ComboValue;
            ScoreText.text = "Score: " + PlayerScore.ToString("F0");
        }
        else
        {
            ScoreText.text = "";
            //MoneyText.text = "Money: " + PlayerMoney;

        }




    }

    public void PickupCoin()
    {
        PlayerScore += 20 * ComboValue;
        PlayerMoney += 50000 * CoinValue * ComboValue;
        PlayerPrefs.SetFloat("Money", PlayerMoney);
        FindObjectOfType<SkinMenu>().SaveMoney();
        ComboValue = 1;
    }

    public void KillEnnemy()
    {
        PlayerScore += 100 * ComboValue;
    }

}
