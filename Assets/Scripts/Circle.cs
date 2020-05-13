using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private int CirclesNb;

    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float bonusSpeed = 1;

    public bool IsObstacle;
    [Tooltip("0: Obstacle Helice / 1: Circle de base / 2: Bumper / 3: Obstacle du + au - cassé / 8: Mur etape")]
    public Sprite[] sprites;
    SpriteRenderer spriterenderer;


    private float rotationSpeed;

    private GameObject BilleObj;
    private float BilleZ;
    private float Xmovement;
    private float maxXmovement;
    private ScoreManager sm;
    private GameManager gm;
    private GameObject Player;

    public void Start()
    {
        gm = GameManager.Instance;
        sm = ScoreManager.Instance;
        CirclesNb = gm.CirclesNumber;
        Player = GameObject.FindWithTag("Player");
        spriterenderer = GetComponent<SpriteRenderer>();
        speed = gm.InitialCircleSpeed;

        ResetCircle();
        BilleObj = gm.PlayerObj;
        BilleZ = BilleObj.transform.position.z;
 


        maxXmovement = BilleObj.GetComponent<BilleMovement>().width * 8;
    }

    public void Update()
    {
        if (gm.IsPlayerDead) return;

        if(transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsMurEtape == true && transform.GetChild(0).gameObject.transform.position.z < gm.CirclesNumber/3)
        {
           // transform.GetChild(0).gameObject.transform.eulerAngles = new Vector3(0, 0, Player.GetComponent<BilleMovement>().angle * 60);
            transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0, 0, Player.GetComponent<BilleMovement>().angle * 60);

            if ((transform.GetChild(0).gameObject.transform.rotation.z+180 >= 360 || transform.GetChild(0).gameObject.transform.rotation.z-180 <= 360))
            {
                spriterenderer.sprite = sprites[1];
                transform.GetChild(0).gameObject.GetComponent<Obstacle>().MurEtapeCollider.enabled = false;
                transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsMurEtape = false;
                transform.GetChild(0).gameObject.SetActive(false);
                gm.NewLevel(gm.LevelNb + 1);
                print("mur etape detruit");
            }
        }

        Xmovement = -((Camera.main.WorldToScreenPoint(BilleObj.transform.position).x - Screen.width/2) / Screen.width) * maxXmovement;

        transform.position -= new Vector3(0, 0,Time.deltaTime * speed);

        transform.position = new Vector3(Xmovement * ((transform.position.z/CirclesNb)) , transform.position.y, transform.position.z);
        transform.rotation *= Quaternion.Euler(0,0,rotationSpeed * speed);

        
        if (transform.position.z <= BilleZ)
        {
            spriterenderer.sortingOrder = 0;
        }

        if (transform.position.z <= 0)
        {
            ResetCircle();
            transform.position = new Vector3(0, 0, CirclesNb);
        }

        
        //speed += Time.deltaTime * bonusSpeed;
    }

    public void ChangeBonusSpeed(float newBonusSpeed)
    {
        bonusSpeed = newBonusSpeed;
        speed = bonusSpeed;
    }


    public void ResetCircle()
    {

        IsObstacle = Random.Range(0, 4) == 1;

        transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsBumper = false;
        transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsMurEtape = false;
        transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsMuret = false;


        transform.GetChild(0).gameObject.GetComponent<Obstacle>().HP = gm.ObstacleHP;
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).gameObject.GetComponent<Obstacle>().MurEtapeCollider.enabled = false;


        if (Mathf.RoundToInt(sm.PlayerScore) % 20 == 0 && Mathf.RoundToInt(sm.PlayerScore) >= 10)
        {
            print("next level");
            transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsMurEtape = true;
            transform.GetChild(0).gameObject.GetComponent<Obstacle>().MuretCollider.enabled = false;
            transform.GetChild(0).gameObject.GetComponent<Obstacle>().HeliceCollider.enabled = false;
            transform.GetChild(0).gameObject.GetComponent<Obstacle>().MurEtapeCollider.enabled = true;
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;

            transform.GetChild(0).gameObject.SetActive(true);
        }else if (IsObstacle)
        {
            transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsMuret = Random.Range(0, 2) == 1;
            transform.GetChild(0).gameObject.GetComponent<Obstacle>().MuretCollider.enabled = transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsMuret;
            transform.GetChild(0).gameObject.GetComponent<Obstacle>().HeliceCollider.enabled = !transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsMuret;
            if (transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsMuret)
            {
                spriterenderer.sprite = sprites[7];
            }
            else
            {
                spriterenderer.sprite = sprites[0];
            }

            transform.GetChild(0).gameObject.SetActive(true);
            
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            spriterenderer.sprite = sprites[1];
        }

        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

        rotationSpeed = Time.deltaTime * Random.Range(-20, 20);

        CoinSpawner.Instance.SpawnCoin();
    }
}