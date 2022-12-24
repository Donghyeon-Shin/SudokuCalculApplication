using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManger : MonoBehaviour
{
    public AudioSource musicSource, btnSource;
    public Slider musicSlider, effectSlider;
    public Button musicButton, effectButton;
    public Sprite musicIcon, musicMuteIcon, effectIcon1, effectIcon2, effectIcon3, effectMuteIcon; // Icon Img

    float beforeMusicVolume = 1, beforeEffectVolume = 1;

    // Convert sound when you click the music icon
    public void ClickMusicIcon()
    {
        if (musicSource.volume == 0)
        {
            musicSource.volume = beforeMusicVolume;
            musicSlider.value = beforeMusicVolume;
        }
        else
        {
            musicSource.volume = 0;
            musicSlider.value = 0;
        }

        changeMusicIcon();
    }

    // Convert sound when you click the effect icon
    public void ClickEffectIcon()
    {
        if (btnSource.volume == 0)
        {
            btnSource.volume = beforeEffectVolume;
            effectSlider.value = beforeEffectVolume;
        }
        else
        {
            btnSource.volume = 0;
            effectSlider.value = 0;
        }

        changeEffectIcon();
    }

    // Setting BackGounnd Music Using Slider UI 
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        if (volume != 0) beforeMusicVolume = volume;

        changeMusicIcon();
    }

    // Setting Button Effect Volume Using Slider UI 
    public void SetBtnVolume(float volume)
    {
        btnSource.volume = volume;
        if (volume != 0) beforeEffectVolume = volume;

        changeEffectIcon();
    }

    // Setting play Button ( Test Btn Volume )
    public void OnSfx()
    {
        btnSource.Play();
    }

    // Change icon according to musicSource volume
    private void changeMusicIcon()
    {
        if (musicSource.volume == 0) musicButton.image.sprite = musicMuteIcon;
        else musicButton.image.sprite = musicIcon;
    }

    // Change icon according to btnSource volume
    private void changeEffectIcon()
    {
        if (btnSource.volume == 0) effectButton.image.sprite = effectMuteIcon;
        else if (btnSource.volume < 0.3) effectButton.image.sprite = effectIcon1;
        else if (btnSource.volume < 0.6) effectButton.image.sprite = effectIcon2;
        else effectButton.image.sprite = effectIcon3;
    }
}
