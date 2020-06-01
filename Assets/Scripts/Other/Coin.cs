using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [HideInInspector]
    public int MaxZ;
    private ObjectsMovementManager omm;

    public GameObject Particle;

    void Start()
    {
        omm = ObjectsMovementManager.Instance;
        MaxZ = GameManager.Instance.CirclesNumber;
        
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }



    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.HasGameStarted || GameManager.Instance.IsPlayerDead) Destroy(this.gameObject);
  

        transform.position = omm.GetNextPos(this.transform.position);

        if (transform.position.z < 0)
        {
            LayerManager.Instance.AllActiveSprites.Remove(GetComponent<SpriteRenderer>());

            Destroy(this.gameObject);
        }
    }
}