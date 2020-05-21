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
    private GameObject childCircle;
    private Obstacle childObstacle;

    public void Awake()
    {
        childCircle = this.gameObject.transform.GetChild(0).gameObject;
        childObstacle = childCircle.GetComponent<Obstacle>();
    }

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

        if(childObstacle.IsMurEtape == true && childCircle.transform.position.z < (gm.CirclesNumber/3) + bonusSpeed * 2)
        {
           // childCircle.transform.rotation = Quaternion.Euler(0, 0, (Player.GetComponent<BilleMovement>().angle - baseAngle) * 60);
            //childCircle.transform.localRotation = Quaternion.Euler(0, 0, (Player.GetComponent<BilleMovement>().angle - baseAngle) * 60);
           
            
            //gameObject.transform.localRotation = Quaternion.Euler(0, 0, (Player.GetComponent<BilleMovement>().angle - baseAngle) * 60);


            // Debug.Log("ANGLE ISSOUMs: " + Vector3.Angle(Player.transform.position, baseRot));
            //Debug.Log("ANGLE ISSOUMs: " + childCircle.transform.rotation.z);

            // Debug.Log("ANGLE ISSOUMs: " + Player.GetComponent<BilleMovement>().angle);

            // if (Vector3.Angle(Player.transform.position, baseRot) > 350)
            //if ((childCircle.transform.rotation.z - baseZRot) >= 350 || (childCircle.transform.rotation.z - baseZRot) <= -350)
            if (Player.GetComponent<BilleMovement>().angle - baseAngle > 6 || Player.GetComponent<BilleMovement>().angle - baseAngle < -6)
            {
                gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);

                //spriterenderer.sprite = sprites[1];

                Color previous = previousCircle.spriterenderer.material.GetColor("_Color");

                

                spriterenderer.material = materials[1];
                spriterenderer.material.SetColor("_Color", previous);

                childCircle.GetComponent<Obstacle>().MurEtapeCollider.enabled = false;
                childCircle.GetComponent<Obstacle>().IsMurEtape = false;
                childCircle.SetActive(false);
                gm.NewLevel(gm.LevelNb + 1);
               /// print("mur etape detruit");
            }
        }else if(childObstacle.IsMurEtape == true && childCircle.transform.position.z > gm.CirclesNumber / 3)
        {
            baseAngle = Player.GetComponent<BilleMovement>().angle;
            baseRot = childCircle.transform.up;
            baseZRot = gameObject.transform.rotation.z;
        }
        

        Xmovement = -((Camera.main.WorldToScreenPoint(BilleObj.transform.position).x - Screen.width/2) / Screen.width) * maxXmovement;

        transform.position -= new Vector3(0, 0,Time.deltaTime * speed * bonusSpeed);

        transform.position = new Vector3(Xmovement * ((transform.position.z/CirclesNb) ) , transform.position.y, transform.position.z);

        if (childObstacle.IsMurEtape == false) // SI MUR ETAPE ON ROTATEA PAS ISSOU
        {
            transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed * speed * Time.timeScale);
        }


        if (transform.position.z <= BilleZ)
        {
            spriterenderer.material.SetFloat("_Alpha", Mathf.Lerp(spriterenderer.material.GetFloat("_Alpha"), 0, Time.deltaTime * 3f * gm.LevelSpeed));
            childCircle.GetComponent<SpriteRenderer>().material.SetFloat("_Alpha", Mathf.Lerp(childCircle.GetComponent<SpriteRenderer>().material.GetFloat("_Alpha"), 0, Time.deltaTime * 3f * gm.LevelSpeed));

           
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
        childCircle.GetComponent<Obstacle>().IsBumper = false;
        childCircle.GetComponent<Obstacle>().IsMurEtape = false;
        childCircle.GetComponent<Obstacle>().IsMuret = false;

  
        childCircle.GetComponent<Obstacle>().HP = gm.ObstacleHP;
        childCircle.GetComponent<SpriteRenderer>().enabled = false;
        childCircle.GetComponent<Obstacle>().MurEtapeCollider.enabled = false;
        childCircle.GetComponent<Obstacle>().MuretCollider.enabled = false;
        childCircle.GetComponent<Obstacle>().HeliceCollider.enabled = false;

        childCircle.SetActive(false);

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
            childCircle.SetActive(false);
            
            spriterenderer.material = materials[1];
            AssignNewColor(Time.deltaTime * 0.2f);


            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));


          rotationSpeed = Time.deltaTime * Random.Range(-20, 20);

          CoinSpawner.Instance.SpawnCoin(CirclesNb);
          return;
        }
        IsObstacle = Random.Range(0, 80) <= 5 + gm.LevelNb*3;

        childCircle.GetComponent<Obstacle>().IsBumper = false;
        childCircle.GetComponent<Obstacle>().IsMurEtape = false;
        childCircle.GetComponent<Obstacle>().IsMuret = false;

        childCircle.GetComponent<Obstacle>().IsJesusCross = false;


        childCircle.GetComponent<Obstacle>().HP = gm.ObstacleHP;
        childCircle.GetComponent<SpriteRenderer>().enabled = false;
        childCircle.GetComponent<Obstacle>().MurEtapeCollider.enabled = false;
        childCircle.GetComponent<Obstacle>().JesusCollider2.enabled = false;


        if (Mathf.RoundToInt(sm.distance) % (gm.CirclesNumber + Mathf.RoundToInt(gm.LevelSpeed)) == 0 && Mathf.RoundToInt(sm.PlayerScore) >= 10)
        {
            if(previousCircle.IsObstacle == true)
            {
                previousCircle.ResetObstacle();
            }
            if(previousCircle.previousCircle.IsObstacle == true)
            {
                previousCircle.previousCircle.ResetObstacle();
            }
            if (previousCircle.previousCircle.previousCircle.IsObstacle == true)
            {
                previousCircle.previousCircle.previousCircle.ResetObstacle();
            }
            if (previousCircle.previousCircle.previousCircle.previousCircle.IsObstacle == true)
            {
                previousCircle.previousCircle.previousCircle.previousCircle.ResetObstacle();
            }

            if (sm.PlayerScore - sm.lastScore < 10) return;
            sm.lastScore = sm.PlayerScore;
          
            childCircle.GetComponent<Obstacle>().IsMurEtape = true;
            childCircle.GetComponent<Obstacle>().MuretCollider.enabled = false;
            childCircle.GetComponent<Obstacle>().HeliceCollider.enabled = false;
            childCircle.GetComponent<Obstacle>().MurEtapeCollider.enabled = true;
            childCircle.GetComponent<Obstacle>().JesusCollider.enabled = false;
            childCircle.GetComponent<Obstacle>().JesusCollider2.enabled = false;
            childCircle.GetComponent<SpriteRenderer>().enabled = true;

        
            childCircle.GetComponent<SpriteRenderer>().material = materials[4];
            childCircle.GetComponent<SpriteRenderer>().material.SetColor("_Color", fx.ShiftedColor(0.25f) * 6f);

            spriterenderer.material = materials[6];
            spriterenderer.material.SetColor("_Color", fx.ShiftedColor(Time.deltaTime * 0.2f) * 1f);
            
            fx.ChangePreviousColor(fx.ShiftedColor(Time.deltaTime * 0.2f + 0.25f));

            //childCircle.GetComponent<SpriteRenderer>()
            //spriterenderer.sprite = sprites[1];

            childCircle.SetActive(true);
        }
        else if (IsObstacle)
        {
            childCircle.GetComponent<Obstacle>().IsMuret = Random.Range(0, 4) <= 1;
            childCircle.GetComponent<Obstacle>().MuretCollider.enabled = childObstacle.IsMuret;
            childCircle.GetComponent<Obstacle>().HeliceCollider.enabled = !childObstacle.IsMuret;
            childCircle.GetComponent<Obstacle>().JesusCollider.enabled = false;
            childCircle.GetComponent<Obstacle>().JesusCollider2.enabled = false;
            childCircle.GetComponent<Obstacle>().IsJesusCross = false;
            if (childCircle.GetComponent<Obstacle>().IsMuret)
            {

                childCircle.GetComponent<Obstacle>().MuretCollider.enabled = true;
                childCircle.GetComponent<Obstacle>().HeliceCollider.enabled = false;
                childCircle.GetComponent<Obstacle>().MurEtapeCollider.enabled = false;
                childCircle.GetComponent<SpriteRenderer>().enabled = true;
                childCircle.GetComponent<SpriteRenderer>().material = materials[3];
                //childCircle.GetComponent<SpriteRenderer>()
                //spriterenderer.sprite = sprites[1];
                spriterenderer.material = materials[1];
                childCircle.SetActive(true);
  
            }
            else if(Random.Range(0,4)<3) // IS HELICE
            {
                childCircle.GetComponent<Obstacle>().MuretCollider.enabled = false;
                childCircle.GetComponent<Obstacle>().HeliceCollider.enabled = true;
                childCircle.GetComponent<Obstacle>().MurEtapeCollider.enabled = false;
                childCircle.GetComponent<SpriteRenderer>().enabled = true;
                childCircle.GetComponent<SpriteRenderer>().material = materials[0];
                //childCircle.GetComponent<SpriteRenderer>()
                //spriterenderer.sprite = sprites[1];
                spriterenderer.material = materials[1];

            }
            else // IS JESUS
            {
                childCircle.GetComponent<Obstacle>().MuretCollider.enabled = false;
                childCircle.GetComponent<Obstacle>().HeliceCollider.enabled = false;
                childCircle.GetComponent<Obstacle>().MurEtapeCollider.enabled = false;
                childCircle.GetComponent<Obstacle>().JesusCollider.enabled = true;
                childCircle.GetComponent<Obstacle>().JesusCollider2.enabled = true;
                childCircle.GetComponent<SpriteRenderer>().enabled = true;
                childCircle.GetComponent<SpriteRenderer>().material = materials[5];
                //childCircle.GetComponent<SpriteRenderer>()
                //spriterenderer.sprite = sprites[1];
                spriterenderer.material = materials[1];
                childCircle.GetComponent<Obstacle>().IsJesusCross = true;
            }

            childCircle.SetActive(true);
            AssignNewColor(Time.deltaTime * 0.2f);

        }
        else
        {
            childCircle.SetActive(false);
            //spriterenderer.sprite = sprites[1];
            spriterenderer.material = materials[1];
            AssignNewColor(Time.deltaTime * 0.2f);
        }

        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));


        rotationSpeed = Time.deltaTime * Random.Range(-20, 20);
        if (childCircle.GetComponent<Obstacle>().IsMurEtape == true)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            childCircle.transform.rotation = Quaternion.Euler(0, 0, 0);
            rotationSpeed = 0;
        }
            CoinSpawner.Instance.SpawnCoin(CirclesNb);

        LayerManager.Instance.SetCurrentDepth();

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