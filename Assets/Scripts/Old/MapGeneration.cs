using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public float PlayerSpeed;
    public List<Transform> PlayerRoute = new List<Transform>();
    public GameObject BentCylinder, StraightCylinder;
    public GameObject Player;

    public Transform lastTransform;

    public float interpolation = 0;

    void Start()
    {
        //InstantiatePipe(Player.transform);
        //lastTransform = Player.transform;
    }

    void Update()
    {
        //interpolation += Time.deltaTime * PlayerSpeed;
        //interpolation = Mathf.Clamp(interpolation, 0, 1);

        //if (!IsOver(PlayerRoute[0]))
        //{
        //    Player.transform.position = Vector3.Lerp(lastTransform.position, PlayerRoute[0].position, interpolation);
        //    Quaternion rot;
        //    rot = Quaternion.Lerp(lastTransform.rotation, PlayerRoute[0].rotation, interpolation);
        //    rot *= Quaternion.Inverse(Quaternion.Euler(0, 0, PlayerRoute[0].rotation.z));

        //    Player.transform.rotation = Quaternion.Lerp(lastTransform.rotation, rot, interpolation);

        //    //Player.transform.forward = Quaternion.ToEulerAngles(rot);
        //}

        //if (PlayerRoute.Count <= 4)
        //{
        //    InstantiatePipe(LastCheckpoint());
        //}
    }

    GameObject InstantiatePipe(Transform lastTransform)
    {

        bool isBent = Random.Range(0, 2) == 0;

        Quaternion newRotation = lastTransform.rotation;

        if (isBent)
            newRotation *= Quaternion.Euler(0, 0, Random.Range(0, 360));

        GameObject Instantiated;

        if (isBent)
        {
            Instantiated = Instantiate(BentCylinder, lastTransform.position, newRotation);
        }
        else
        {
            Instantiated = Instantiate(StraightCylinder, lastTransform.position, newRotation);
        }

        

        return Instantiated;
    }

    public bool IsOver(Transform _transform)
    {

        if (interpolation >= 1 - Time.deltaTime)
        {
            lastTransform = _transform;

            if (PlayerRoute.Contains(_transform))
                PlayerRoute.Remove(_transform);

            interpolation = 0;

            return true;
        }

        else
        {
            return false;
        }
    }

    public Transform LastCheckpoint()
    {
        //PlayerRoute[PlayerRoute.Count - 1].rotation *= Quaternion.Inverse(Quaternion.Euler(0, 0, PlayerRoute[PlayerRoute.Count - 1].eulerAngles.z));
        return PlayerRoute[PlayerRoute.Count -1];
    }
}