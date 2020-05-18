using System.Collections;
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
    private ImageEffectController fx;
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
        fx = ImageEffectController.Instance;
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

        Color newLevelColor = new Color();

        if (levelNb > 1)
        {
         /*   newLevelColor = fx.ShiftedColor(Time.deltaTime * 2f);
            fx.ChangePreviousColor(newLevelColor);*/

            LevelUpText.SetActive(true);
            LevelUpText.GetComponent<Animator>().Play("TextFade", 0, 0);
        }

        ScoreManager.Instance.ComboValue = 1;

        //fx.ShiftedColor(0.5f) * 4f; // On assigne la couleur du nouveau niveau, ne tkt pas ça va bien se passer, bien se passer ne tkt pas

        LevelNb = levelNb;
        LevelSpeed = levelNb*0.4f +2;

        foreach (GameObject go in generator.Circles)
        {
            go.GetComponent<Circle>().ChangeBonusSpeed(LevelSpeed);
            if (levelNb > 1)
            {
                /* for (int i = generator.Circles.Count; i > 0; i--)
                 {
                     if (i == generator.Circles.Count)
                         generator.Circles[0].GetComponent<Circle>().AssignNewColor(Time.deltaTime * 0.5f);
                  //   else
                 //        generator.Circles[i].GetComponent<Circle>().AssignNewColor(Time.deltaTime);
                 }*/

                go.GetComponent<SpriteRenderer>().material.SetColor("_Color", newLevelColor * 4f);           // on s'emmerde pas, on assigne la même couleur
                                                                                                        //pour tous les cercles présents

            }
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