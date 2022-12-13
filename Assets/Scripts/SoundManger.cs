using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource btnSource;

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetBtnVolume(float volume)
    {
        btnSource.volume = volume;
    }

    public void OnSfx()
    {
        btnSource.Play();
    }
}
