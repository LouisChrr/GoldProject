using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGenerator : MonoBehaviour
{
    private int CirclesNb;
    public GameObject Circle;
    public LayerManager layerManager;

    [HideInInspector]
    public List<GameObject> Circles = new List<GameObject>();

    private GameObject Player;

    public bool canDestroy = true;

    void Start()
    {
        layerManager = LayerManager.Instance;
        Player = GameManager.Instance.PlayerObj;
        CirclesNb = GameManager.Instance.CirclesNumber;
        for (int i = 0; i < CirclesNb; i++)
        {
            GameObject newCircle = Instantiate(Circle, new Vector3(0, 0, 1 * i), Quaternion.identity, GameManager.Instance.Circles);
            
            if (i < CirclesNb / 8)
            {
                // pas d'obstacle au debut
                newCircle.GetComponent<Circle>().preventObstacleAtStart = true;
                newCircle.GetComponent<Circle>().Start();
                newCircle.GetComponent<Circle>().ResetObstacle(false);
            }
                 if(i != 0)
            {
                newCircle.GetComponent<Circle>().previousCircle = Circles[i - 1].GetComponent<Circle>();
            }
                 if(i == CirclesNb - 1)
            {
                Circles[0].GetComponent<Circle>().previousCircle = newCircle.GetComponent<Circle>();
            }

            Circles.Add(newCircle);
            layerManager.AllActiveSprites.Add(newCircle.GetComponent<SpriteRenderer>());
            layerManager.AllActiveSprites.Add(newCircle.transform.GetChild(0).GetComponent<SpriteRenderer>());
        }
    }

    public Circle GetNearest()
    {
        float maxDist = CirclesNb;
        Circle nearest = new Circle();

        foreach (GameObject circle in Circles)
        {
            if (circle.transform.position.z < maxDist)
            {
                maxDist = circle.transform.position.z;
                nearest = circle.GetComponent<Circle>();
            }
        }

        return nearest;
    }
}

/*
 * 
 * __________████████_____██████
_________█░░░░░░░░██_██░░░░░░█
________█░░░░░░░░░░░█░░░░░░░░░█
_______█░░░░░░░███░░░█░░░░░░░░░█
_______█░░░░███░░░███░█░░░████░█
______█░░░██░░░░░░░░███░██░░░░██
_____█░░░░░░░░░░░░░░░░░█░░░░░░░░███
____█░░░░░░░░░░░░░██████░░░░░████░░█
____█░░░░░░░░░█████░░░████░░██░░██░░█
___██░░░░░░░███░░░░░░░░░░█░░░░░░░░███
__█░░░░░░░░░░░░░░█████████░░█████████
_█░░░░░░░░░░█████_████___████_█████___█
_█░░░░░░░░░░█______█_███__█_____███_█___█
█░░░░░░░░░░░░█___████_████____██_██████
░░░░░░░░░░░░░█████████░░░████████░░░█
░░░░░░░░░░░░░░░░█░░░░░█░░░░░░░░░░░░█
░░░░░░░░░░░░░░░░░░░░██░░░░█░░░░░░██
░░░░░░░░░░░░░░░░░░██░░░░░░░███████
░░░░░░░░░░░░░░░░██░░░░░░░░░░█░░░░░█
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█
░░░░░░░░░░░█████████░░░░░░░░░░░░░░██
░░░░░░░░░░█▒▒▒▒▒▒▒▒███████████████▒▒█
░░░░░░░░░█▒▒███████▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█
░░░░░░░░░█▒▒▒▒▒▒▒▒▒█████████████████
░░░░░░░░░░████████▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█
░░░░░░░░░░░░░░░░░░██████████████████
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█
██░░░░░░░░░░░░░░░░░░░░░░░░░░░██
▓██░░░░░░░░░░░░░░░░░░░░░░░░██
▓▓▓███░░░░░░░░░░░░░░░░░░░░█
▓▓▓▓▓▓███░░░░░░░░░░░░░░░██
▓▓▓▓▓▓▓▓▓███████████████▓▓█
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓█
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓█
*/