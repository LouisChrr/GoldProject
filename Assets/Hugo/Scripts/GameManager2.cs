using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public GameObject _panelPause;

    public GameObject imgPause;
    public GameObject imgPlay;

    public GameObject menu;
    public GameObject options;
    public GameObject game;

    public GameObject greyPanel;

    public AudioSource audio1;

    private bool _isPause;

    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 1;

        _panelPause.SetActive(false);

        imgPause.SetActive(true);
        imgPlay.SetActive(false);

        menu.SetActive(true);
        options.SetActive(false);
        game.SetActive(false);

        greyPanel.SetActive(true);

        audio1.Play();

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

    public void Options()
    {
        options.SetActive(true);
        menu.SetActive(false);

    }

    public void Menu()
    {
        menu.SetActive(true);
        options.SetActive(false);

    }

    public void DoPause()
    {
        Pause(!_isPause);
    }

    public void DoPlay()
    {
        menu.SetActive(false);
        greyPanel.SetActive(false);
        game.SetActive(true);
    }

    private void Pause(bool pause)
    {
        _isPause = pause;

        if (pause)
        {
            Time.timeScale = 0;
            imgPause.SetActive(false);
            imgPlay.SetActive(true);

            audio1.Pause();

            Debug.Log("On Pause");
        }
        else
        {
            Time.timeScale = 1;
            imgPause.SetActive(true);
            imgPlay.SetActive(false);

            audio1.UnPause();

            Debug.Log("On Play");
        }

        _panelPause.SetActive(_isPause);
    }
}
