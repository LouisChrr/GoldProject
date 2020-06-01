using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsMovementManager : MonoBehaviour
{
    public static ObjectsMovementManager Instance;
    private float Xmovement;
    private float maxXmovement;
    private Transform BilleObj;
    private float speed;
    public float bonusSpeed;
    private int CirclesNb;
    public float bulletSpeed;
    private Camera cam;
    private float screenWidth;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cam = Camera.main;
        screenWidth = Screen.width;
        CirclesNb = GameManager.Instance.CirclesNumber;
        bonusSpeed = GameManager.Instance.LevelSpeed;
        BilleObj = GameManager.Instance.PlayerObj.transform ;
        speed = GameManager.Instance.InitialCircleSpeed;
        maxXmovement = BilleObj.GetComponent<BilleMovement>().width * 8;
    }


    /// <summary>
    /// Next pos for Circle, Bullet & Coin ISSOUMe
    /// </summary>
    /// <param name="basePos">transform.position de l'objet original</param>
    /// <param name="isBullet"></param>
    /// <param name="bulletBaseX"></param>
    /// <returns>obj next pos</returns>
    public Vector3 GetNextPos(Vector3 basePos, bool isBullet = false, float bulletBaseX = 0)
    {
        // GERE LES MOUVEMENTS DE COINS, CIRCLE, BULLETS
        Xmovement = -((cam.WorldToScreenPoint(BilleObj.position).x - screenWidth / 2) / screenWidth) * maxXmovement;
        if (!isBullet)
        {
            basePos -= new Vector3(0, 0, Time.deltaTime * speed * bonusSpeed);
        }
        else
        {
            basePos += new Vector3(0, 0, Time.deltaTime * bulletSpeed * bonusSpeed);
        }

        return( new Vector3(bulletBaseX+(Xmovement * ((basePos.z / CirclesNb))), basePos.y, basePos.z));

    }
}