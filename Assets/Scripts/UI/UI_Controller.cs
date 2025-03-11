using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] PrometeoCarController carController;
    [SerializeField] DriftController driftController;
    [SerializeField] CarController carDataController;

    EventManager _eventManager;

    private GameObject mobileControllers;

    private void Start()
    {
        _eventManager = FindObjectOfType<EventManager>();
        mobileControllers = _eventManager.GetMobileControllers();

        SetCurrentCar();

        if (_eventManager != null)
        {
            UpdateSpeedText();
        }
    }

    private void UpdateSpeedText()
    {
        carController.carSpeedText = _eventManager.CheckSpeedText();
        driftController.driftText = _eventManager.CheckDriftText();
    }

    private void SetCurrentCar()
    {
        carDataController.CurrentCarSpawn(_eventManager.GetCurrentCar());
    }

    public void SetCarController(PrometeoCarController newCarController)
    {
        carController = newCarController;

        if (_eventManager.IsMobile())
        {
            carController.useTouchControls = true;
            carController.throttleButton = mobileControllers.transform.GetChild(0).gameObject;
            carController.reverseButton = mobileControllers.transform.GetChild(1).gameObject;
            carController.handbrakeButton = mobileControllers.transform.GetChild(2).gameObject;
            carController.turnLeftButton = mobileControllers.transform.GetChild(3).gameObject;
            carController.turnRightButton = mobileControllers.transform.GetChild(4).gameObject;
            mobileControllers.SetActive(true);
        }
    }

    public void SetDriftController(DriftController newDriftController)
    {
        driftController = newDriftController;
    }
}
