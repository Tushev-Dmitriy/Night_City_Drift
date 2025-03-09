using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

        CinemachineCameraOffset tempCinemachineOffset = carCamera.GetComponent<CinemachineCameraOffset>();
        tempCinemachineOffset.m_Offset = carData.cameraOffset;

        PrometeoCarController tempCarController = tempCar.GetComponentInChildren<PrometeoCarController>();
        tempCarController.maxSpeed = carData.carCharacteristics.maxSpeed;
        tempCarController.accelerationMultiplier = carData.carCharacteristics.engineLvl;
        tempCarController.maxSteeringAngle = carData.carCharacteristics.steeringAngleLvl;
        tempCarController.brakeForce = carData.carCharacteristics.brakeLvl;
        
        if (carData.carCharacteristics.haveTurbine)
        {
            tempCarController.accelerationMultiplier += 2;
        }

        DriftController tempDriftController = tempCar.GetComponentInChildren<DriftController>();

        UI_Controller tempUIController = uiController.GetComponent<UI_Controller>();
        tempUIController.SetCarController(tempCarController);
        tempUIController.SetDriftController(tempDriftController);
    }

    public void SetCarData(MainCarData newCarData)
    {
        carData = newCarData;
        bodyColor = newCarData.carView.carColor.color;
        wheelColor = newCarData.carView.wheelColor.color;
    }
}
