using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private MainCamera mainCam;
    public GameObject mainMenuHolder;

    void Start()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<MainCamera>();
    }

    public void PlayGame()
    {
        mainCam.GameStarted = true;
        mainMenuHolder.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
