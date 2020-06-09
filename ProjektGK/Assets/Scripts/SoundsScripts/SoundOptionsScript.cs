//author: Dawid Musialik
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

// author Dawid Musialik

public class SoundOptionsScript : MonoBehaviour
{
    public static bool PausedGame;
    public GameObject PauseMenuUI;
    public GameObject SoundOptionsMenuUI; 
    public GameObject SoundManager;
    public Slider MusicVolumeSlider;
    public Slider EffectsVolumeSlider;
    SoundEffect[] sounds;


     void Start()
    {
        sounds = SoundManager.GetComponent<SoundManager>().SoundEffects;
        foreach (var effect in sounds)
        {
            if (effect.AudioName == "IndoorMusic" || effect.AudioName == "OutdoorMusic")
                MusicVolumeSlider.value = effect.ClipAudioSource.volume;
            else
                EffectsVolumeSlider.value = effect.ClipAudioSource.volume;
        }
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PausedGame && SoundOptionsMenuUI.activeSelf == true)
            {
                Return();
            }
        }
    }


    public void ChangeMusicVolume()
    {
        Debug.Log("Music Volume Changed");
        SoundManager.GetComponent<SoundManager>().musicVolume = MusicVolumeSlider.value;
        SoundManager.GetComponent<SoundManager>().UpdateVolume();
    }

    public void ChangeEffectsVolume()
    {
        Debug.Log("Effects Volume Changed");

        SoundManager.GetComponent<SoundManager>() .effectsVolume = EffectsVolumeSlider.value;
        SoundManager.GetComponent<SoundManager>().UpdateVolume();
    }

    public void Return()
    {
        PauseMenuUI.SetActive(true);
        SoundOptionsMenuUI.SetActive(false);
    }
}

