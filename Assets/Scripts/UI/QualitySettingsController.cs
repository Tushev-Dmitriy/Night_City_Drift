using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QualitySettingsController : MonoBehaviour
{
    [SerializeField] Slider qualitySlider;

    public void OnValueChange()
    {
        QualitySettings.SetQualityLevel((int)qualitySlider.value);
    }
}
