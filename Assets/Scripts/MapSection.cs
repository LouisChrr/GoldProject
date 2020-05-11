using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSection : MonoBehaviour
{
    public float speed = 1;

    public bool IsObstacle;
    public Sprite[] sprites;
    SpriteRenderer spriterenderer;
    SpriteRenderer bigmamaspriterenderer;
    public GameObject circle;

    public float rotationSpeed;

    private GameObject BilleObj;
    private float DragonBilleZ;
    public float Xmovement;
    public float maxXmovement;
    public float expFn;

    public void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        bigmamaspriterenderer = GetComponentInChildren<SpriteRenderer>();
        speed = 1;

        ResetCircle();
        BilleObj = GameObject.FindGameObjectWithTag("Player");
        DragonBilleZ = BilleObj.transform.position.z;
        //Vector3 widthToWorld = Camera.main.ScreenToWorldPoint(new Vector3((Screen.width), 0, 0));


        maxXmovement = BilleObj.GetComponent<BilleMovement>().width * 8;
    }




    public void Update()
    {
        Xmovement = ((Camera.main.WorldToScreenPoint(BilleObj.transform.position).x - Screen.width/2) / Screen.width) * maxXmovement;

        transform.position -= new Vector3(0, 0,Time.deltaTime * speed);


        

        transform.position = new Vector3(Xmovement * ((transform.position.z/30)) , transform.position.y, transform.position.z);
        transform.rotation *= Quaternion.Euler(0,0,rotationSpeed * speed);

        
        if (transform.position.z <= DragonBilleZ)
        {
            spriterenderer.sortingOrder = 0;
            bigmamaspriterenderer.sortingOrder = 0;
        }

        if (transform.position.z <= 0)
        {
            IsObstacle = Random.Range(0, 4) == 1;
            ResetCircle();
            transform.position = new Vector3(0, 0, 30);
        }



    }


    public void ResetCircle()
    {
        spriterenderer.sortingOrder = -1;
        bigmamaspriterenderer.sortingOrder = 0;

        if (IsObstacle)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            spriterenderer.sprite = sprites[0];
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            spriterenderer.sprite = sprites[1];
        }

        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

        rotationSpeed = Time.deltaTime * Random.Range(-20, 20);
    }
}