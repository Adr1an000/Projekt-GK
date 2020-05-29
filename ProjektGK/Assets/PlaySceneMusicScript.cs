﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneMusicScript : MonoBehaviour
{
    public string SceneMusic;

    private void Start()
    {
        FindObjectOfType<SoundManager>().PlayMusic(SceneMusic);
    }
}
