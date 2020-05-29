using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public SoundEffect[] SoundEffects;

    private void Awake()
    {
        foreach(var effect in SoundEffects)
        {
            effect.ClipAudioSource = gameObject.AddComponent<AudioSource>();
            effect.ClipAudioSource.volume = effect.effectVolume;
            effect.ClipAudioSource.clip = effect.EffectSound;
            effect.ClipAudioSource.loop = effect.IsLooped;
        }
    }



    public void PlayMusic(string musicName)
    {
        SoundEffect soundEffect = Array.Find(SoundEffects, s => s.AudioName == musicName);

        if(soundEffect != null)
        {
            soundEffect.ClipAudioSource.Play();
        }
    }
}
