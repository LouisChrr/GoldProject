using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{



    [HideInInspector]
    public int MaxZ;
    private GameObject BilleObj;
    private int CirclesNb;
    private float Xmovement;
    private float maxXmovement;
    private float baseX;
    private float bonusSpeed, speed;

    void Start()
    {
        MaxZ = GameManager.Instance.CirclesNumber;
        BilleObj = GameManager.Instance.PlayerObj;
        CirclesNb = GameManager.Instance.CirclesNumber;
        maxXmovement = BilleObj.GetComponent<BilleMovement>().width * 8;
        baseX = transform.position.x;
        bonusSpeed = GameManager.Instance.LevelSpeed;
        speed = GameManager.Instance.InitialCircleSpeed;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }



    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.HasGameStarted || GameManager.Instance.IsPlayerDead) Destroy(this.gameObject);
        bonusSpeed = GameManager.Instance.LevelSpeed;

        transform.position -= new Vector3(0, 0, Time.deltaTime * bonusSpeed* speed); // 

        Xmovement = -((Camera.main.WorldToScreenPoint(BilleObj.transform.position).x - Screen.width / 2) / Screen.width) * maxXmovement;
        transform.position = new Vector3(baseX + (Xmovement * ((transform.position.z / CirclesNb))), transform.position.y, transform.position.z);

        if (transform.position.z < 0)
        {
            LayerManager.Instance.AllActiveSprites.Remove(GetComponent<SpriteRenderer>());
            Destroy(this.gameObject);
        }
    }
}
