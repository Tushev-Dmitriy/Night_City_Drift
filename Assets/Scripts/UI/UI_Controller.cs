using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] PrometeoCarController carController;
    UI_Manager _uiManager;

    private void Start()
    {
        _uiManager = FindObjectOfType<UI_Manager>();

        if (_uiManager != null)
        {
            UpdateSpeedText();
        }
    }

    void UpdateSpeedText()
    {
        carController.carSpeedText = _uiManager.CheckSpeedText();
    }

    public void SetCarController(PrometeoCarController newCarController)
    {
        carController = newCarController;
    }
}
