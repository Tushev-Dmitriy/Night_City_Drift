using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetColorToCar : MonoBehaviour
{
    [SerializeField] private MaterialGameEvent colorChangedEvent;
    [SerializeField] private Image materialImg;
    [SerializeField] private Button acceptColorBtn;

    private void Awake()
    {
        acceptColorBtn.onClick.AddListener(SetNewColor);
    }

    private void SetNewColor()
    {
        Material newCarMaterial = materialImg.material;
        colorChangedEvent.Raise(newCarMaterial);
    }
}
