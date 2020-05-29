using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneName = "ShootingRange";


    public void Play()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Help()
    {
        SceneManager.LoadScene("Help");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
