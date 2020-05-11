using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class BilleMovement : MonoBehaviour
{
    [Header("Adjust values here")]
    public CircleGenerator generator;
    public Text ScoreText91;
    public Text WinScoreText91;
    public GameObject DeathUI;
    public bool IsDead = false;
    public float Score = 0;
    public Text speedtxt;
    public float maxSpeed;
    public float speedDecreaseSmooth;
    public float width, height;
    public float angle;
    private Vector3 targetPos;
    private float speedLerpTimer;
    private Animator BilleAnimator;
    public GameObject touchFeedback;// --- A DELETE --- //////
    [Header("Do not modify!")]
    public float speed;
 

    // Mobile device related
    private Touch touch;
    private int screenWidth;
    private float dragOrigin;
    // Start is called before the first frame update

    private void Awake()
    {
        Vector3 widthToWorld = GetWorldPositionOnPlane(new Vector3((Screen.width - (Screen.width / 8)), 0, 0), 1);
        //print(widthToWorld);
        width = widthToWorld.x;
        height = width;
    }

    void Start()
    {
        screenWidth = Screen.width;

        //Vector3 widthToWorld = Camera.main.ScreenToWorldPoint(new Vector3((screenWidth), 0, 0));

        

        touchFeedback.transform.position = new Vector3(0.0f, -3.0f, 0.0f);// --- A DELETE --- //////
        angle = -1.5f; // Set de la bille en bas au milieu
        targetPos.z = transform.position.z;
        targetPos.x = Mathf.Cos(angle) * width;
        targetPos.y = Mathf.Sin(angle) * height;
        transform.localPosition = targetPos;

        BilleAnimator = GetComponent<Animator>();


    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerUpdate();
        if (IsDead) return;
        BilleUpdate();
    }

    void PlayerUpdate()
    {

        if (!IsDead)
        {
            Score += Time.deltaTime* 2.0f;
            ScoreText91.text = "Ton score : " + Score.ToString("F0");

            foreach (GameObject go in generator.Circles)
            {
                go.GetComponent<MapSection>().speed += Time.deltaTime * Score * 0.001f;
            }
        }
        
    }

    void BilleUpdate()
    {
        // Touch input
        SpeedTouchDragInput();
        speedtxt.text = "SPEED: " + speed.ToString("F2") + "      MAXSPEED: " + maxSpeed;

        if (speed == 0) return;
        // Déplacement de la bille suivant un cercle (width, height)
        angle += Time.deltaTime * speed;
        targetPos.x = Mathf.Cos(angle) * width;
        targetPos.y = Mathf.Sin(angle) * height;
        //targetPos.z = transform.position.z;
        transform.localPosition = targetPos;

        //print(Mathf.Atan2(Mathf.Cos(angle) * Mathf.Rad2Deg, Mathf.Sin(angle) * Mathf.Rad2Deg) * Mathf.Rad2Deg + " Atan2");
        // Rotation locale de la bille pour toujours orienter le bas vers le cylindre
        transform.localEulerAngles = new Vector3(0, 0, (-Mathf.Atan2(Mathf.Cos(angle) * Mathf.Rad2Deg, Mathf.Sin(angle) * Mathf.Rad2Deg) * Mathf.Rad2Deg) - 180.0f);

        BilleAnimator.speed = (Mathf.Abs(speed)/10.0f + 2f);
    }

    void SpeedTouchDragInput()
    {
        if (Input.touchCount > 0) // Si un ou plusieurs doigts détectés
        {
            touch = Input.GetTouch(0); // On prend le premier doigt
            if (touch.phase == TouchPhase.Moved) // Si il a bougé, update le speed
            {
                speed = ((touch.position.x - dragOrigin) / screenWidth) * maxSpeed;
                
            }
            else if(touch.phase == TouchPhase.Began) // Si il vient d'arriver, update le dragOrigin
            {
                dragOrigin = touch.position.x;
              //  touchFeedback.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).y, 0.0f) * 2;// --- A DELETE --- //////
               

            }
            else if(touch.phase == TouchPhase.Ended) // Si il part, on prépare le lerp du speed vers 0
            {
                speedLerpTimer = 0;
            }
            
            touchFeedback.GetComponent<RectTransform>().localPosition = new Vector3(touch.position.x - screenWidth / 2.0f, touch.position.y - Screen.height/2, 0.0f);
        }
        else // Si pas d'input détecté
        {
            if (speedLerpTimer < 0.96f) // Si toujours en cours de lerp vers 0
            {
                speedLerpTimer += Time.deltaTime * (1/speedDecreaseSmooth);
                speed = Mathf.Lerp(speed, 0, speedLerpTimer);
            }
            else
            {
                speedLerpTimer = 0;
                speed = 0;
            }
            touchFeedback.GetComponent<RectTransform>().localPosition = new Vector3(0.0f, -1000.0f,0.0f) *  2; // --- A DELETE --- //////
        }

        speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed); // Clamp du speed selon maxSpeed
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "DangerZone")
        {
            Death();
        }
    }

    public void Death()
    {
        IsDead = true;
        WinScoreText91.text = "Ton score : " + Score.ToString("F0");
        DeathUI.SetActive(IsDead);
        foreach (GameObject go in generator.Circles)
        {
            go.GetComponent<MapSection>().speed = 0;
        }

        touchFeedback.SetActive(false);

    }

}
