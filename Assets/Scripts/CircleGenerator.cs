using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGenerator : MonoBehaviour
{
    public GameObject Circle;
    public List<GameObject> Circles = new List<GameObject>();

    public GameObject Player;

    public bool canDestroy = true;

    void Start()
    {

        for (int i = 0; i < 30; i++)
        {
            GameObject newCircle = Instantiate(Circle, new Vector3(0, 0, 1 * i), Quaternion.identity);

            if (i < 4)
                newCircle.GetComponent<MapSection>().IsObstacle = false;

            Circles.Add(newCircle);
        }
    }

    void Update()
    {
    }
}