using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [Header("Cars data")]
    [SerializeField] List<MainCarData> _carsData;

    [SerializeField] TMP_Text speedText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public TMP_Text CheckSpeedText()
    {
        return speedText;
    }

    public List<MainCarData> GetCarsData()
    {
        return _carsData;
    }
}