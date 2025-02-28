using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor.Search;

public class CarController : MonoBehaviour
{
    [Header ("Scene objects")]
    [SerializeField] GameObject cityObject;
    [SerializeField] GameObject uiController;
    [SerializeField] GameObject carCamera;

    [Header("Car Data")]
    [SerializeField] MainCarData carData;
    [SerializeField] Color bodyColor;
    [SerializeField] Color wheelColor;

    public void CurrentCarSpawn(MainCarData currentCarData)
    {
        carData = currentCarData;
        SetCarData(carData);
        SetupCarStats();
    }

    private void SetupCarStats()
    {
        GameObject tempCar = Instantiate(carData.carObject, cityObject.transform);
        CinemachineVirtualCamera tempCinemachineData = carCamera.GetComponent<CinemachineVirtualCamera>();
        tempCinemachineData.Follow = tempCar.transform.GetChild(0).transform;
        tempCinemachineData.LookAt = tempCar.transform.GetChild(0).transform;
        PrometeoCarController tempCarController = tempCar.GetComponentInChildren<PrometeoCarController>();
        tempCarController.maxSpeed = carData.carCharacteristics.maxSpeed;
        tempCarController.accelerationMultiplier = carData.carCharacteristics.engineLvl;
        tempCarController.maxSteeringAngle = carData.carCharacteristics.steeringAngleLvl;
        tempCarController.brakeForce = carData.carCharacteristics.brakeLvl;

        uiController.GetComponent<UI_Controller>().SetCarController(tempCarController);
    }

    public void SetCarData(MainCarData newCarData)
    {
        carData = newCarData;
        bodyColor = newCarData.carView.carColor.color;
        wheelColor = newCarData.carView.wheelColor.color;
    }
}
