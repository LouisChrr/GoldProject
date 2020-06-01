using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public int MaxZ;
    private float baseX;
    [HideInInspector]
    public Transform InactiveBullets;
    [HideInInspector]
    public Transform ActiveBullets;
    private GameManager gm;
    private ObjectsMovementManager omm;

    // Start is called before the first frame update
    void Start()
    {
        omm = ObjectsMovementManager.Instance;
        gm = GameManager.Instance;
        MaxZ = gm.CirclesNumber;
  
        baseX = transform.position.x;
  
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.HasGameStarted || gm.IsPlayerDead) Destroy(this.gameObject);

        transform.position = omm.GetNextPos(this.transform.position, true, baseX);
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

            if (Obstacle.GetComponent<Obstacle>().IsJesusCross || Obstacle.GetComponent<Obstacle>().IsMurEtape || Obstacle.GetComponent<Obstacle>().IsBumper || Obstacle.GetComponent<Obstacle>().HP == 0 || Obstacle.GetComponent<Obstacle>().HeliceCollider.enabled == true) return;
            gm.PlayerAudioSource.PlayOneShot(gm.PlayerAudioClips[3]);
            Obstacle.GetComponent<Obstacle>().HP -= 1;
            Obstacle.GetComponentInParent<Obstacle>().SetSprite();

            ParticleSystemRenderer pr = Obstacle.transform.parent.GetChild(1).GetComponent<ParticleSystemRenderer>();

            pr.material.SetColor("_Color",Obstacle.GetComponent<SpriteRenderer>().material.GetColor("_Color"));
            Obstacle.transform.parent.GetChild(1).GetComponent<ParticleSystem>().Play();


        }
    }

}