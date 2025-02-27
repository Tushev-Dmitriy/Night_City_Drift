using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetNewCar : MonoBehaviour
{
    [SerializeField] private GameEvent carSwapChangedEvent;
    [SerializeField] private bool isIncrease;
    [SerializeField] private Button acceptColorBtn;

    private void Awake()
    {
        acceptColorBtn.onClick.AddListener(SetNewCarData);
    }

    private void SetNewCarData()
    {
        carSwapChangedEvent.Raise(isIncrease);
    }
}
