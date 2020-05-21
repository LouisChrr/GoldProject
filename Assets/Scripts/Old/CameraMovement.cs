using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject BilleObj;
    public float maxZoom;
    public float deadZoneAsRatio;
    private float baseZoom;
    private Camera cam;
    private float lerpRatio;
    private Vector3 basePos, newPos;
    private float circleWidth;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        //baseZoom = cam.orthographicSize;
        baseZoom = cam.fieldOfView;

        basePos = cam.transform.position;
        circleWidth = BilleObj.GetComponent<BilleMovement>().width;
        newPos = new Vector3(circleWidth/2.0f, basePos.y, basePos.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        //lerpRatio = Mathf.Abs(((cam.WorldToScreenPoint(BilleTransform.position).x - screenHalfWidth) / (screenHalfWidth)));
        lerpRatio = Mathf.Clamp(Mathf.Abs((BilleObj.transform.position.x / circleWidth)) - deadZoneAsRatio, 0,1);

        //cam.orthographicSize = Mathf.Lerp(baseZoom, maxZoom, Mathf.Abs(lerpRatio));
        cam.fieldOfView = Mathf.Lerp(baseZoom, maxZoom, Mathf.Abs(lerpRatio));


        lerpRatio *= Mathf.Sign(BilleObj.transform.position.x);
        cam.transform.position = Vector3.SlerpUnclamped(basePos, newPos, lerpRatio);
    }
}
