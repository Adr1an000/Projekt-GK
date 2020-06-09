using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// author Dawid Musialik

public class LevelSwitch : MonoBehaviour
{
    public Animator animator;

    public string goToNameLevel;

    public float waitTime = 0;

    private bool sceneWillChange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Payload")
        {
            sceneWillChange = true;
        }
    }

    private void Update()
    {
        if (!sceneWillChange)
        {
            return;
        }

        waitTime -= Time.deltaTime;

        if (waitTime <= 0)
        {
            animator.SetBool("FadeOut", true);

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1
            && animator.GetCurrentAnimatorStateInfo(0).IsName("Fade_OUT"))
            {
                SceneManager.LoadScene(goToNameLevel);
            }
        }
    }
}
