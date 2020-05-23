using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameScript : MonoBehaviour
{
    public static bool PausedGame;

    public GameObject PauseMenuUI;

    public GameObject PlayerHudUI;

    public GameObject CursorObject;

    public GameObject CameraObject;

    void Start()
    {
        PausedGame = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!PausedGame)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    private void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        PlayerHudUI.SetActive(false);

        CursorObject.GetComponent<CursorScript>().visible = true;
        CameraObject.GetComponent<W_MouseScript>().enabled = false;

        Time.timeScale = 0f;

        PausedGame = true;
    }

    public void ResumeGame()
    {
        Debug.Log("Resume");

        PauseMenuUI.SetActive(false);
        PlayerHudUI.SetActive(true);

        CursorObject.GetComponent<CursorScript>().visible = false;
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

    public void ExitGame()
    {
        Debug.Log("Exit");

        Application.Quit();
    }
}
