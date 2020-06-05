using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    public AudioSource PlayerAudioSource;
    public List<AudioClip> PlayerAudioClips;

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

    public int nbDeaths = 0;
    public int nbDestroyedWallsTotal;


    public CircleGenerator generator;


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

        AssignColorToAll();

        LoadNbDeathsAndWalls();
        //LoadNbWalls();
    }

    private void AssignColorToAll()
    {
        List<GameObject> sortedList = generator.Circles.OrderBy(g => g.transform.position.z).ToList();

        Color color = ImageEffectController.Instance.ShiftedColor(0.25f);

        int i = 0;

        foreach (GameObject go in sortedList)
        {

            //  go.GetComponent<Circle>().ChangeBonusSpeed(LevelSpeed);
            // DUMB, ON GERE CA DANS ObjectsMovementManager MTN
            // ok boomer


            if (LevelNb > 0)
            {
                color = ImageEffectController.Instance.ShiftedColor(Time.deltaTime);
                go.GetComponent<Circle>().ResetColor(color);
            }
            i++;
        }

        ImageEffectController.Instance.ChangePreviousColor(color);
    }

    public void StartGame()
    {
        IsPlayerDead = false;
        HasGameStarted = true;
        //foreach (GameObject go in generator.Circles)
        //{
        //    go.GetComponent<Circle>().ChangeBonusSpeed(0);
        //}
        //ScoreManager.Instance.PlayerMoney = 0;
        NewLevel(1);
    }

    public void NewLevel(int levelNb) // A APPELER A CHAQUE NEW LEVEL ISSOUm
    {

        if (levelNb > 1)
        {
            AchievementsManager.Instance.AddLevelPassed(levelNb - 1);
            LevelUpText.SetActive(true);
            LevelUpText.GetComponent<Animator>().Play("TextFade", 0, 0);
            Handheld.Vibrate();
        }

        ScoreManager.Instance.ComboValue = 1;

        LevelNb = levelNb;
        LevelSpeed = levelNb*0.8f +2;
        ObjectsMovementManager.Instance.bonusSpeed = LevelSpeed;

        //AssignColorToAll();
    }

    public void Death()
    {
        Handheld.Vibrate();
        ScoreManager.Instance.PlayerMoney += ScoreManager.Instance.MoneyInGame;
        ScoreManager.Instance.totalMoney += ScoreManager.Instance.MoneyInGame;

        PlayerAudioSource.PlayOneShot(PlayerAudioClips[1]);
        if (am == null) am = AchievementsManager.Instance;
        AchievementsManager.Instance.AddScore(Mathf.RoundToInt(ScoreManager.Instance.PlayerScore));
        AchievementsManager.Instance.AddDistance(Mathf.RoundToInt(ScoreManager.Instance.distance));

        AchievementsManager.Instance.AddDeath(1);

        IsPlayerDead = true;
        MenuManager.Instance.EndGame();
        ObjectsMovementManager.Instance.bonusSpeed = 0;

        SaveSystem.SaveMoney(ScoreManager.Instance);

        nbDeaths++;

        if (nbDeaths <= 10)
        {
            UIScript.Instance.DieAchievementIncrement1();
        }

        if (nbDeaths <= 100)
        {
            UIScript.Instance.DieAchievementIncrement2();
        }

        if (nbDeaths <= 500)
        {
            UIScript.Instance.DieAchievementIncrement3();
        }

        nbDestroyedWallsTotal += BilleMovement.Instance.nbDestroyedWallsInGame;
        //SaveNbWalls();

        SaveNbDeathsAndNbWalls();

        if (nbDestroyedWallsTotal <= 10)
        {
            UIScript.Instance.DestroyAchievementIncrement1();
        }

        if (nbDestroyedWallsTotal <= 50)
        {
            UIScript.Instance.DestroyAchievementIncrement2();
        }

        if (nbDestroyedWallsTotal <= 100)
        {
            UIScript.Instance.DestroyAchievementIncrement3();
        }

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
    
    //public void SaveNbWalls()
    //{
    //    SaveSystem.SaveNbWalls(BilleMovement.Instance);
    //}

    //public void LoadNbWalls()
    //{
    //    DataScript data = SaveSystem.LoadNbWalls();
        
    //    nbDestroyedWallsTotal = data.walls;

    //}
    
    public void SaveNbDeathsAndNbWalls()
    {
        SaveSystem.SaveNbDeathsAndNbWalls(this);
    }

    public void LoadNbDeathsAndWalls()
    {
        DataScript data = SaveSystem.LoadNbDeathsAndNbWalls();

        nbDeaths = data.deaths;
        nbDestroyedWallsTotal = data.walls;

    }
}