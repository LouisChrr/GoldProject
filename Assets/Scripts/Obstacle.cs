using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public BoxCollider HeliceCollider, MuretCollider, MurEtapeCollider;
    public int HP;
    public bool IsMuret;
    public bool IsBumper;
    public bool IsMurEtape;

    private SpriteRenderer ParentSprite;
    private Circle ParentCircle;

    public void Start()
    {
        ParentSprite = transform.parent.GetComponent<SpriteRenderer>();
        ParentCircle = transform.parent.GetComponent<Circle>();
    }

    public void SetSprite()
    {
       // GetComponentInParent<SpriteRenderer>().sprite = GetComponentInParent<Circle>().sprites[HP + 2];

        ParentSprite.material = ParentCircle.materials[HP + 2];
        if(HP <= 0)
        {
            IsBumper = true;
            IsMuret = false;
         
            ScoreManager.Instance.KillEnnemy();
        }

        // Sprite[2] le bumper
        // Apres: sprite du moins d'HP au plus d'HP 
    }

}