using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] PrometeoCarController carController;

    UI_Manager _uiManager;
    
    [Inject]
    public void Construct(UI_Manager ui_Manager)
    {
        _uiManager = ui_Manager;
    } 

    private void Start()
    {
        if (_uiManager == null)
        {
            Debug.LogError("UI_Manager is not injected!");
            return;
        }

        if (carController == null)
        {
            Debug.LogError("CarController is not assigned!");
            return;
        }

        carController.carSpeedText = _uiManager.CheckTest();
    }
}
