using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float bonusSpeed = 1;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public int MaxZ;
    private GameObject BilleObj;
    private int CirclesNb;
    private float Xmovement;
    private float maxXmovement;
    private float baseX;
    [HideInInspector]
    public Transform InactiveBullets;
    [HideInInspector]
    public Transform ActiveBullets;

    // Start is called before the first frame update
    void Start()
    {
        MaxZ = GameManager.Instance.CirclesNumber;
        BilleObj = GameManager.Instance.PlayerObj;
        CirclesNb = GameManager.Instance.CirclesNumber;
        maxXmovement = BilleObj.GetComponent<BilleMovement>().width * 8;
        baseX = transform.position.x;
        bonusSpeed = GameManager.Instance.LevelSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, Time.deltaTime * speed * bonusSpeed);

        Xmovement = -((Camera.main.WorldToScreenPoint(BilleObj.transform.position).x - Screen.width / 2) / Screen.width) * maxXmovement;
        transform.position = new Vector3(baseX + (Xmovement * ((transform.position.z / CirclesNb))), transform.position.y, transform.position.z);


        if (transform.position.z >= MaxZ) this.transform.parent = InactiveBullets;
    }

    public void ResetBullet(Transform PlayerTransform)
    {
        baseX = PlayerTransform.position.x;
 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            GameObject Obstacle = collision.transform.gameObject;
            this.transform.parent = InactiveBullets;

            if (!Obstacle.GetComponent<Obstacle>().IsMuret && !Obstacle.GetComponent<Obstacle>().IsBumper) return;
            Obstacle.GetComponent<Obstacle>().HP -= 1;
            Obstacle.GetComponentInParent<Obstacle>().SetSprite();
            if (Obstacle.GetComponent<Obstacle>().HP == 0)
            {
                Obstacle.GetComponentInParent<Obstacle>().IsMuret = false;
                Obstacle.GetComponentInParent<Obstacle>().IsBumper = true;
            }
            else if (Obstacle.GetComponent<Obstacle>().HP == -1)
            {
                ScoreManager.Instance.ComboValue *= 2;
                Obstacle.GetComponentInParent<Circle>().IsObstacle = false;
                Obstacle.GetComponent<Obstacle>().IsBumper = false;
                Obstacle.GetComponent<Obstacle>().MuretCollider.enabled = false;
                Obstacle.GetComponentInParent<Circle>().GetComponent<SpriteRenderer>().sprite = Obstacle.GetComponentInParent<Circle>().sprites[1];
                Obstacle.SetActive(false);
            }

            // Destroy(this.gameObject);
            
        }
    }

}
