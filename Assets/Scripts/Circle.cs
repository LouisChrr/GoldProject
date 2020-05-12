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
    public Sprite[] sprites;
    SpriteRenderer spriterenderer;


    private float rotationSpeed;

    private GameObject BilleObj;
    private float BilleZ;
    private float Xmovement;
    private float maxXmovement;
    private ScoreManager sm;
    private GameManager gm;

    public void Start()
    {
        gm = GameManager.Instance;
        sm = ScoreManager.Instance;
        CirclesNb = gm.CirclesNumber;

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




        spriterenderer.sortingOrder = -1;

        transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsBumper = false;
        transform.GetChild(0).gameObject.GetComponent<Obstacle>().HP = gm.ObstacleHP;

        // Spawner des obstacle Ismuret true et false
        // Et des coinPrefab en haut du tunnel

        if (IsObstacle)
        {
            transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsMuret = Random.Range(0, 2) == 1;
            transform.GetChild(0).gameObject.GetComponent<Obstacle>().MuretCollider.enabled = transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsMuret;
            transform.GetChild(0).gameObject.GetComponent<Obstacle>().HeliceCollider.enabled = !transform.GetChild(0).gameObject.GetComponent<Obstacle>().IsMuret;

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

        CoinSpawner.Instance.SpawnCoin();
    }
}