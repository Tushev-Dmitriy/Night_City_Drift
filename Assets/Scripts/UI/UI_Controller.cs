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

    private void Start()
    {
        _eventManager = FindObjectOfType<EventManager>();

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
    }

    public void SetDriftController(DriftController newDriftController)
    {
        driftController = newDriftController;
    }
}
