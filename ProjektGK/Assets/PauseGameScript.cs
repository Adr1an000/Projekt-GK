using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameScript : MonoBehaviour
{
    public static bool PausedGame;

    public GameObject PauseMenuUI;

    public GameObject PlayerHudUI;

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

        Time.timeScale = 0f;

        PausedGame = true;
    }

    public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        PlayerHudUI.SetActive(true);

        Time.timeScale = 1f;

        PausedGame = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
