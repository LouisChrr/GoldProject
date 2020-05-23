﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private int CirclesNb;

    public bool IsObstacle;
    [Tooltip("0: Helice / 1: Circle de base / 2: Bumper / 3: Obstacle du + au - cassé / 7: Mur etape / 8: JesusCross")]
    public Sprite[] sprites;

    public Material[] materials;

    SpriteRenderer spriterenderer;
    private float baseAngle;

    private float rotationSpeed;

    private float BilleZ;

    private ScoreManager sm;
    private ImageEffectController fx;
    private GameManager gm;
    private GameObject Player;

    public bool preventObstacleAtStart = false;

    public Circle previousCircle;
    private GameObject childCircle;
    private Obstacle childObstacle;
    private ObjectsMovementManager omm;

    public void Awake()
    {
        childCircle = this.gameObject.transform.GetChild(0).gameObject;
        childObstacle = childCircle.GetComponent<Obstacle>();
    }

    public void Start()
    {
        omm = ObjectsMovementManager.Instance;
        gm = GameManager.Instance;
        sm = ScoreManager.Instance;
        fx = ImageEffectController.Instance;
        CirclesNb = gm.CirclesNumber;
        Player = GameObject.FindWithTag("Player");
        spriterenderer = GetComponent<SpriteRenderer>();
 

        ResetCircle(preventObstacleAtStart);

        BilleZ = gm.PlayerObj.transform.position.z;


    }

    public void Update()
    {
        if (gm.IsPlayerDead || !gm.HasGameStarted) return;

        if(childObstacle.IsMurEtape == true && childCircle.transform.position.z < (gm.CirclesNumber/3) + gm.LevelSpeed * 2)
        {
            if (Player.GetComponent<BilleMovement>().angle - baseAngle > 6 || Player.GetComponent<BilleMovement>().angle - baseAngle < -6)
            {
                gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);

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

        }

        transform.position = omm.GetNextPos(this.transform.position);
        if (childObstacle.IsMurEtape == false) // SI MUR ETAPE ON ROTATEA PAS ISSOU
        {
            transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed * Time.timeScale);
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

                spriterenderer.material = materials[1];
                childCircle.GetComponent<Obstacle>().IsJesusCross = true;
            }

            childCircle.SetActive(true);
            AssignNewColor(Time.deltaTime * 0.2f);

        }
        else
        {
            childCircle.SetActive(false);

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