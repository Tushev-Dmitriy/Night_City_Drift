using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] PrometeoCarController carController;
    EventManager _eventManager;

    private void Start()
    {
        _eventManager = FindObjectOfType<EventManager>();

        if (_eventManager != null)
        {
            UpdateSpeedText();
        }
    }

    void UpdateSpeedText()
    {
        carController.carSpeedText = _eventManager.CheckSpeedText();
    }

    public void SetCarController(PrometeoCarController newCarController)
    {
        carController = newCarController;
    }
}
