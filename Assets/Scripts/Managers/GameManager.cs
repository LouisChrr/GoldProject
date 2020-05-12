using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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

    public GameObject DeathUI; // TEMPO, A REMPLACER PAR UN MENUMANAGERT

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
        
        sm = ScoreManager.Instance;
        NewLevel(LevelNb);
    }

    public void NewLevel(int levelNb) // A APPELER A CHAQUE NEW LEVEL ISSOUm
    {
        LevelSpeed = levelNb * 2;
        foreach (GameObject go in generator.Circles)
        {
            go.GetComponent<Circle>().bonusSpeed = LevelSpeed;
        }

    }

    public void Death()
    {
        IsPlayerDead = true;

        DeathUI.SetActive(IsPlayerDead);

        foreach (GameObject go in generator.Circles)
        {
            go.GetComponent<Circle>().speed = 0;
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