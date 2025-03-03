using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [Header("Mixer")]
    [SerializeField] AudioMixer mainMixer;

    [Header("UI")]
    [SerializeField] Slider carMixerSlider;
    [SerializeField] Slider musicMixerSlider;

    private UserData _userData;

    public void SetupSound(UserData userData)
    {
        _userData = userData;
        SetupSliderValue(true);
    }

    public void ChangeVolume(int numOfMixer)
    {
        switch (numOfMixer)
        {
            case 0:
                mainMixer.SetFloat("Car", carMixerSlider.value);
                SetupSliderValue(false);
                break;
            case 1:
                mainMixer.SetFloat("Music", musicMixerSlider.value);
                SetupSliderValue(false);
                break;
        }
    }

    private void SetupSliderValue(bool isLoad)
    {
        if (isLoad)
        {
            carMixerSlider.value = _userData.carVolume;
            musicMixerSlider.value = _userData.musicVolume;
            ChangeVolume(0);
            ChangeVolume(1);
        } else
        {
            _userData.SetCarVolume(carMixerSlider.value);
            _userData.SetMusicVolume(musicMixerSlider.value);
        }
    }
}
