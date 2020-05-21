using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public BoxCollider HeliceCollider, MuretCollider, MurEtapeCollider, JesusCollider, JesusCollider2;
    public int HP;
    public bool IsMuret;
    public bool IsBumper;
    public bool IsMurEtape;
    public bool IsJesusCross;

    private SpriteRenderer ParentSprite;
    private Circle ParentCircle;
    private SpriteRenderer sr;

    public void Start()
    {
        ParentSprite = transform.parent.GetComponent<SpriteRenderer>();
        ParentCircle = transform.parent.GetComponent<Circle>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetSprite()
    {
       // GetComponentInParent<SpriteRenderer>().sprite = GetComponentInParent<Circle>().sprites[HP + 2];
       if(IsMuret || IsBumper)
        {
            sr.material = ParentCircle.materials[HP + 2];
        }
        

     

        if (IsJesusCross)
        {
            sr.material = ParentCircle.materials[8];
        }
     

        if(HP == 0)
        {
            IsBumper = true;
            IsMuret = false;
            sr.material = ParentCircle.materials[2];
            ScoreManager.Instance.KillEnnemy();
        }else if (HP < 0)
        {
            sr.material = ParentCircle.materials[1];
            IsBumper = false;
            //HP = GameManager.Instance.ObstacleHP;
        }

        // Sprite[2] le bumper
        // Apres: sprite du moins d'HP au plus d'HP 
    }

}