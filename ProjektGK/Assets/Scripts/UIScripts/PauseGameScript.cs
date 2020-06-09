
﻿//authors: Paweł Salicki, Dawid Musialik

using System.Collections;

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameScript : MonoBehaviour
{
    public static bool PausedGame;

    public GameObject PauseMenuUI;

    public GameObject SoundOptionsMenuUI;

    public GameObject PlayerHudUI;

    public GameObject CursorObject;

    public GameObject CameraObject;

    void Start()
    {
        PausedGame = false;
    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PausedGame && PauseMenuUI.activeSelf == false)
            {
                PauseGame();
            }
            else if (PauseMenuUI.activeSelf == true)
            {
                ResumeGame();
            }


        }
    }

    private void PauseGame()
    {

        Debug.Log("Pause");
        PauseMenuUI.SetActive(true);

        PauseMenuUI.SetActive(true);

        CursorObject.GetComponent<CursorScript>().visible = true;
        CursorObject.GetComponent<CursorScript>().mode = CursorLockMode.None;
        CameraObject.GetComponent<W_MouseScript>().enabled = false;

        Time.timeScale = 0f;

        PausedGame = true;
    }

    public void ResumeGame()
    {
        Debug.Log("Resume");

        PauseMenuUI.SetActive(false);

        CursorObject.GetComponent<CursorScript>().visible = false;
        CursorObject.GetComponent<CursorScript>().mode = CursorLockMode.Locked;
        CameraObject.GetComponent<W_MouseScript>().enabled = true;

        Time.timeScale = 1f;

        PausedGame = false;
    }

    public void MainMenu()
    {
        Debug.Log("Main menu");

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void SoundOptionsMenu()
    {
        Debug.Log("Options");

        PauseMenuUI.SetActive(false);
        SoundOptionsMenuUI.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Exit");

        Application.Quit();
    }

}

