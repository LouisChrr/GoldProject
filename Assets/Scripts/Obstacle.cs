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

    public void SetSprite()
    {
       // GetComponentInParent<SpriteRenderer>().sprite = GetComponentInParent<Circle>().sprites[HP + 2];
        GetComponentInParent<SpriteRenderer>().material = GetComponentInParent<Circle>().materials[HP + 2];

        // Sprite[2] le bumper
        // Apres: sprite du moins d'HP au plus d'HP 
    }

}
