using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public GameObject _panelPause;
    public GameObject _panelMenu;

    public GameObject imgPause;
    public GameObject imgPlay;

    private bool _isPause;

    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 1;

        _panelPause.SetActive(false);
        _panelMenu.SetActive(true);
        imgPause.SetActive(false);
        imgPlay.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MyLoadScene(string nameScene)
    {
        Debug.Log(nameScene);
        UnityEngine.SceneManagement.SceneManager.LoadScene(nameScene);
    }

    public void DoPause()
    {
        Pause(!_isPause);
    }

    public void DoPlay()
    {
        _panelMenu.SetActive(false);
        imgPause.SetActive(true);
    }

    private void Pause(bool pause)
    {
        _isPause = pause;

        if (pause)
        {
            Time.timeScale = 0;
            imgPause.SetActive(false);
            imgPlay.SetActive(true);

            Debug.Log("On Pause");
        }
        else
        {
            Time.timeScale = 1;
            imgPause.SetActive(true);
            imgPlay.SetActive(false);

            Debug.Log("On Play");
        }

        _panelPause.SetActive(_isPause);
    }
}
