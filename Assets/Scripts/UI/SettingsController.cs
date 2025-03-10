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
    [SerializeField] Slider musicMixerSlider;
    [SerializeField] Slider carMixerSlider;

    private UserData _userData;

    public void SetupSound(UserData userData)
    {
        _userData = userData;
        SetupSliderValue(true);
    }

    public void ChangeVolume(int numOfMixer)
    {
        float soundValue = 0;
        switch (numOfMixer)
        {
            case 0:
                soundValue = musicMixerSlider.value;
                mainMixer.SetFloat("Music", soundValue);
                _userData.SetMusicVolume(soundValue);
                break;
            case 1:
                soundValue = carMixerSlider.value;
                mainMixer.SetFloat("Car", soundValue);
                _userData.SetCarVolume(soundValue);
                break;
        }
    }

    private void SetupSliderValue(bool isLoad)
    {
        if (isLoad)
        {
            musicMixerSlider.value = _userData.musicVolume;
            carMixerSlider.value = _userData.carVolume;
            ChangeVolume(0);
            ChangeVolume(1);
        }
    }
}