using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int CoinValue;
    public static ScoreManager Instance;
    private GameManager gm;
    public Text ScoreText;


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
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.IsPlayerDead && gm.HasGameStarted)
        {
            PlayerScore += Time.deltaTime * 2.0f;
            ScoreText.text = "Score: " + PlayerScore.ToString("F0");
        }
        else
        {
            ScoreText.text = "";

        }


    }

    public void PickupCoin()
    {
        PlayerScore += CoinValue/5 * ComboValue;
        PlayerMoney += CoinValue * ComboValue;
        ComboValue = 1;
    }

}
