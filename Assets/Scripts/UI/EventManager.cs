using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    [Header("Cars data")]
    [SerializeField] List<MainCarData> _carsData;

    [Header("Car podium controller")]
    [SerializeField] CarPodiumCotroller carPodiumCotroller;

    [Header("UI elements")]
    [SerializeField] TMP_Text speedText;

    public TMP_Text CheckSpeedText() => speedText;
    public List<MainCarData> GetCarsData() => _carsData;

    private MainCarData currentCarData;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (_carsData.Count > 0 && currentCarData == null)
        {
            currentCarData = _carsData[0];
            OnCarSelected(currentCarData);
        }
    }

    public void OnCarSelected(MainCarData carData)
    {
        currentCarData = carData;
        //speedText.text = $"0";

        carPodiumCotroller.SpawnCar(currentCarData);
    }

    public void OnBodyColorChanged(Material newMaterial)
    {
        if (currentCarData != null)
        {
            currentCarData.carView.carColor.color = newMaterial.GetColor("_Color1");
            carPodiumCotroller.SpawnCar(currentCarData);
        }
    }
}