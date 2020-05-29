using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundEffect
{
    public string AudioName;

    public bool IsLooped = false;

    public AudioClip EffectSound;

    [HideInInspector]
    public AudioSource ClipAudioSource;

    [Range(0.0f, 1.0f)]
    public float effectVolume = 1.0f;
}
