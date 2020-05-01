﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitch : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Payload")
        {
            animator.SetTrigger("FadeOut");
        }
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1
                    && animator.GetCurrentAnimatorStateInfo(0).IsName("Fade_OUT"))
        {

            SceneManager.LoadScene(2);

        }
    }

}
