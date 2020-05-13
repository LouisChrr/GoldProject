using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public float BonusSpeed = 1;

    [HideInInspector]
    public int MaxZ;
    private GameObject BilleObj;
    private int CirclesNb;
    private float Xmovement;
    private float maxXmovement;
    private float baseX;

    void Start()
    {
        MaxZ = GameManager.Instance.CirclesNumber;
        BilleObj = GameManager.Instance.PlayerObj;
        CirclesNb = GameManager.Instance.CirclesNumber;
        maxXmovement = BilleObj.GetComponent<BilleMovement>().width * 8;
        baseX = transform.position.x;
        BonusSpeed = GameManager.Instance.LevelSpeed;
    }



    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, 0, Time.deltaTime * BonusSpeed); // 

        Xmovement = -((Camera.main.WorldToScreenPoint(BilleObj.transform.position).x - Screen.width / 2) / Screen.width) * maxXmovement;
        transform.position = new Vector3(baseX + (Xmovement * ((transform.position.z / CirclesNb))), transform.position.y, transform.position.z);

        if (transform.position.z < 0) Destroy(this.gameObject);
    }
}
