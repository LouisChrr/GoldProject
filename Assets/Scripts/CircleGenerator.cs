using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGenerator : MonoBehaviour
{
    private int CirclesNb;
    public GameObject Circle;

    [HideInInspector]
    public List<GameObject> Circles = new List<GameObject>();

    private GameObject Player;

    public bool canDestroy = true;

    void Start()
    {
        Player = GameManager.Instance.PlayerObj;
        CirclesNb = GameManager.Instance.CirclesNumber;
        for (int i = 0; i < CirclesNb; i++)
        {
            GameObject newCircle = Instantiate(Circle, new Vector3(0, 0, 1 * i), Quaternion.identity, GameManager.Instance.Circles);

            if (i < 4)
                newCircle.GetComponent<Circle>().IsObstacle = false;

            Circles.Add(newCircle);
        }
    }

  
}