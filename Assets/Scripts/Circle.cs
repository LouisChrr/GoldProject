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
    [Tooltip("0: Helice / 1: Circle de base / 2: Bumper / 3: Obstacle du + au - cassé / 7: Mur etape / 8: JesusCross")]
    public Sprite[] sprites;

    public Material[] materials;

    SpriteRenderer spriterenderer;
    private float baseAngle;

    private float rotationSpeed;

    private GameObject BilleObj;
    private float BilleZ;
    private float Xmovement;
    private float maxXmovement;
    private ScoreManager sm;
    private ImageEffectController fx;
    private GameManager gm;
    private GameObject Player;
    private Vector3 baseRot;
    private float baseZRot;
    private float lastScore = 0;
    public bool preventObstacleAtStart = false;
    bool isCible;
    public Circle previousCircle;

    public void Start()
    {
 
        gm = GameManager.Instance;
        sm = ScoreManager.Instance;
        fx = ImageEffectController.Instance;
        CirclesNb = gm.CirclesNumber;
        Player = GameObject.FindWithTag("Player");
        spriterenderer = GetComponent<SpriteRenderer>();
        speed = gm.InitialCircleSpeed;

        ResetCircle(preventObstacleAtStart);
        BilleObj = gm.PlayerObj;
        BilleZ = BilleObj.transform.position.z;
    


               maxXmovement = BilleObj.GetComponent<BilleMovement>().width * 8;
        bonusSpeed = 4;
    }

    public void Update()
    {
        if (gm.IsPlayerDead || !gm.HasGameStarted) return;

        if(transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsMurEtape == true && transform.GetChild(0).gameObject.transform.position.z < (gm.CirclesNumber/3) + bonusSpeed * 2)
        {
           // transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(0, 0, (Player.GetComponent<BilleMovement>().angle - baseAngle) * 60);
            //transform.GetChild(0).gameObject.transform.localRotation = Quaternion.Euler(0, 0, (Player.GetComponent<BilleMovement>().angle - baseAngle) * 60);
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, (Player.GetComponent<BilleMovement>().angle - baseAngle) * 60);

            // Debug.Log("ANGLE ISSOUMs: " + Vector3.Angle(Player.transform.position, baseRot));
            //Debug.Log("ANGLE ISSOUMs: " + transform.GetChild(0).gameObject.transform.rotation.z);

            // Debug.Log("ANGLE ISSOUMs: " + Player.GetComponent<BilleMovement>().angle);

            // if (Vector3.Angle(Player.transform.position, baseRot) > 350)
            //if ((transform.GetChild(0).gameObject.transform.rotation.z - baseZRot) >= 350 || (transform.GetChild(0).gameObject.transform.rotation.z - baseZRot) <= -350)
            if (Player.GetComponent<BilleMovement>().angle - baseAngle > 6 || Player.GetComponent<BilleMovement>().angle - baseAngle < -6)
            {
                gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);

                //spriterenderer.sprite = sprites[1];

                Color previous = previousCircle.spriterenderer.material.GetColor("_Color");

                

                spriterenderer.material = materials[1];
                spriterenderer.material.SetColor("_Color", previous);

                transform.GetChild(0).GetComponent<Obstacle>().MurEtapeCollider.enabled = false;
                transform.GetChild(0).GetComponent<Obstacle>().IsMurEtape = false;
                transform.GetChild(0).gameObject.SetActive(false);
                gm.NewLevel(gm.LevelNb + 1);
               /// print("mur etape detruit");
            }
        }else if(transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsMurEtape == true && transform.GetChild(0).gameObject.transform.position.z > gm.CirclesNumber / 3)
        {
            baseAngle = Player.GetComponent<BilleMovement>().angle;
            baseRot = transform.GetChild(0).transform.up;
            baseZRot = gameObject.transform.rotation.z;
        }
        

        Xmovement = -((Camera.main.WorldToScreenPoint(BilleObj.transform.position).x - Screen.width/2) / Screen.width) * maxXmovement;

        transform.position -= new Vector3(0, 0,Time.deltaTime * speed * bonusSpeed);

        transform.position = new Vector3(Xmovement * ((transform.position.z/CirclesNb) ) , transform.position.y, transform.position.z);

        if (transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsMurEtape == false) // SI MUR ETAPE ON ROTATEA PAS ISSOU
        {
            transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed * speed * Time.timeScale);
        }


        if (transform.position.z <= BilleZ)
        {
            spriterenderer.material.SetFloat("_Alpha", Mathf.Lerp(spriterenderer.material.GetFloat("_Alpha"), 0, Time.deltaTime * 3f * gm.LevelSpeed));
            transform.GetChild(0).GetComponent<SpriteRenderer>().material.SetFloat("_Alpha", Mathf.Lerp(transform.GetChild(0).GetComponent<SpriteRenderer>().material.GetFloat("_Alpha"), 0, Time.deltaTime * 3f * gm.LevelSpeed));

           
            spriterenderer.sortingOrder = 0;
        }

        if (transform.position.z <= 0)
        {
            ResetCircle(false);
            transform.position = new Vector3(0, 0, CirclesNb);
        }

        
        //speed += Time.deltaTime * bonusSpeed;
    }

    public void ChangeBonusSpeed(float newBonusSpeed)
    {
        bonusSpeed = newBonusSpeed;
    }

    public void ResetObstacle()
    {
        // remet un cerlce à sa valeur de base sans obstacle
        transform.GetChild(0).GetComponent<Obstacle>().IsBumper = false;
        transform.GetChild(0).GetComponent<Obstacle>().IsMurEtape = false;
        transform.GetChild(0).GetComponent<Obstacle>().IsMuret = false;

  
        transform.GetChild(0).GetComponent<Obstacle>().HP = gm.ObstacleHP;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<Obstacle>().MurEtapeCollider.enabled = false;
        transform.GetChild(0).GetComponent<Obstacle>().MuretCollider.enabled = false;
        transform.GetChild(0).GetComponent<Obstacle>().HeliceCollider.enabled = false;

        transform.GetChild(0).gameObject.SetActive(false);

        spriterenderer.material = materials[1];
    }


    public void ResetCircle(bool preventObstacle)
    {

        spriterenderer.material.SetFloat("_Alpha", 1);

        // si ct  un obstacle
        if (IsObstacle)
        {
            AchievementsManager.Instance.AddObstacleDodged(1);
        }

        if (preventObstacle)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            
            spriterenderer.material = materials[1];
            AssignNewColor(Time.deltaTime * 0.2f);


            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));


          rotationSpeed = Time.deltaTime * Random.Range(-20, 20);

          CoinSpawner.Instance.SpawnCoin(CirclesNb);
          return;
        }
        IsObstacle = Random.Range(0, 8) == 1;

        transform.GetChild(0).GetComponent<Obstacle>().IsBumper = false;
        transform.GetChild(0).GetComponent<Obstacle>().IsMurEtape = false;
        transform.GetChild(0).GetComponent<Obstacle>().IsMuret = false;

        transform.GetChild(0).GetComponent<Obstacle>().IsJesusCross = false;


        transform.GetChild(0).GetComponent<Obstacle>().HP = gm.ObstacleHP;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<Obstacle>().MurEtapeCollider.enabled = false;
        transform.GetChild(0).GetComponent<Obstacle>().JesusCollider2.enabled = false;


        if (Mathf.RoundToInt(sm.distance) % (gm.CirclesNumber/2 + Mathf.RoundToInt(gm.LevelSpeed)) == 0 && Mathf.RoundToInt(sm.PlayerScore) >= 10)
        {
            
            if (sm.PlayerScore - sm.lastScore < 10) return;
            sm.lastScore = sm.PlayerScore;
          
            transform.GetChild(0).GetComponent<Obstacle>().IsMurEtape = true;
            transform.GetChild(0).GetComponent<Obstacle>().MuretCollider.enabled = false;
            transform.GetChild(0).GetComponent<Obstacle>().HeliceCollider.enabled = false;
            transform.GetChild(0).GetComponent<Obstacle>().MurEtapeCollider.enabled = true;
            transform.GetChild(0).GetComponent<Obstacle>().JesusCollider.enabled = false;
            transform.GetChild(0).GetComponent<Obstacle>().JesusCollider2.enabled = false;
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

        
            transform.GetChild(0).GetComponent<SpriteRenderer>().material = materials[4];
            transform.GetChild(0).GetComponent<SpriteRenderer>().material.SetColor("_Color", fx.ShiftedColor(0.25f) * 16f);

            spriterenderer.material = materials[6];
            spriterenderer.material.SetColor("_Color", fx.ShiftedColor(Time.deltaTime * 0.2f) * 1f);
            
            fx.ChangePreviousColor(fx.ShiftedColor(Time.deltaTime * 0.2f + 0.25f));

            //transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>()
            //spriterenderer.sprite = sprites[1];

            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (IsObstacle)
        {
            transform.GetChild(0).GetComponent<Obstacle>().IsMuret = Random.Range(0, 3) == 0;
            transform.GetChild(0).GetComponent<Obstacle>().MuretCollider.enabled = transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsMuret;
            transform.GetChild(0).GetComponent<Obstacle>().HeliceCollider.enabled = !transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsMuret;
            transform.GetChild(0).GetComponent<Obstacle>().JesusCollider.enabled = false;
            transform.GetChild(0).GetComponent<Obstacle>().JesusCollider2.enabled = false;
            transform.GetChild(0).GetComponent<Obstacle>().IsJesusCross = false;
            if (transform.GetChild(0).GetComponent<Obstacle>().IsMuret)
            {

                transform.GetChild(0).GetComponent<Obstacle>().MuretCollider.enabled = true;
                transform.GetChild(0).GetComponent<Obstacle>().HeliceCollider.enabled = false;
                transform.GetChild(0).GetComponent<Obstacle>().MurEtapeCollider.enabled = false;
                transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                transform.GetChild(0).GetComponent<SpriteRenderer>().material = materials[3];
                //transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>()
                //spriterenderer.sprite = sprites[1];
                spriterenderer.material = materials[1];
                transform.GetChild(0).gameObject.SetActive(true);
  
            }
            else if(Random.Range(0,4)<3) // IS HELICE
            {
                transform.GetChild(0).GetComponent<Obstacle>().MuretCollider.enabled = false;
                transform.GetChild(0).GetComponent<Obstacle>().HeliceCollider.enabled = true;
                transform.GetChild(0).GetComponent<Obstacle>().MurEtapeCollider.enabled = false;
                transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                transform.GetChild(0).GetComponent<SpriteRenderer>().material = materials[0];
                //transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>()
                //spriterenderer.sprite = sprites[1];
                spriterenderer.material = materials[1];

            }
            else // IS JESUS
            {
                transform.GetChild(0).GetComponent<Obstacle>().MuretCollider.enabled = false;
                transform.GetChild(0).GetComponent<Obstacle>().HeliceCollider.enabled = false;
                transform.GetChild(0).GetComponent<Obstacle>().MurEtapeCollider.enabled = false;
                transform.GetChild(0).GetComponent<Obstacle>().JesusCollider.enabled = true;
                transform.GetChild(0).GetComponent<Obstacle>().JesusCollider2.enabled = true;
                transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                transform.GetChild(0).GetComponent<SpriteRenderer>().material = materials[5];
                //transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>()
                //spriterenderer.sprite = sprites[1];
                spriterenderer.material = materials[1];
                transform.GetChild(0).GetComponent<Obstacle>().IsJesusCross = true;
            }

            transform.GetChild(0).gameObject.SetActive(true);
            AssignNewColor(Time.deltaTime * 0.2f);

        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            //spriterenderer.sprite = sprites[1];
            spriterenderer.material = materials[1];
            AssignNewColor(Time.deltaTime * 0.2f);
        }

        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));


        rotationSpeed = Time.deltaTime * Random.Range(-20, 20);
        if (transform.GetChild(0).GetComponent<Obstacle>().IsMurEtape == true)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
            rotationSpeed = 0;
        }
            CoinSpawner.Instance.SpawnCoin(CirclesNb);
    }

    public void AssignNewColor(float shift)
    {
        Color newCol = fx.ShiftedColor(shift);

        fx.ChangePreviousColor(newCol);
        spriterenderer.material.SetColor("_Color", newCol * 4f);
    }


    public void ResetColor(Color color)
    {
        spriterenderer.material.SetColor("_Color", color * 4.0f) ;
    }

}