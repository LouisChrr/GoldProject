using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    

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
        LevelNb = levelNb;
        LevelSpeed = levelNb;
        foreach (GameObject go in generator.Circles)
        {
            go.GetComponent<Circle>().ChangeBonusSpeed(LevelSpeed);
        }

    }



    public void Death()
    {
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