using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPodiumCotroller : MonoBehaviour
{
    [SerializeField] UI_Manager uiManager;
    [SerializeField] GameObject carModelPos;

    List<MainCarData> _carsData;

    private void Awake()
    {
        _carsData = uiManager.GetCarsData();
    }

    private void Update()
    {
        RotateCarModelPos();
    }

    private void RotateCarModelPos()
    {
        carModelPos.transform.Rotate(0, 0.05f, 0);
    }

    private void SwapCar()
    {

    }
}
