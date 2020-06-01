using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinParticle : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, 2);
    }

    void Update()
    {
        //transform.position = ObjectsMovementManager.Instance.GetNextPos(this.transform.position);
        transform.position -= new Vector3(0,0,Time.deltaTime * 3f);
    }
}