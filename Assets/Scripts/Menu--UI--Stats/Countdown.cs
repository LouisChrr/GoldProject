using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{

    public int countdownTime = 3;

    public Text countdown;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public IEnumerator CountDown()
    {

        while(countdownTime <= 0)
        {
            countdown.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdown.gameObject.SetActive(true);

    }
}
