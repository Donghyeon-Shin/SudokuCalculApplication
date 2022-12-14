using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource btnSource;

    // Setting BackGounnd Music Using Slider UI 
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    // Setting Button Effect Volume Using Slider UI 
    public void SetBtnVolume(float volume)
    {
        btnSource.volume = volume;
    }

    // Setting play Button ( Test Btn Volume )
    public void OnSfx()
    {
        btnSource.Play();
    }
}
