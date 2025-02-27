using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetColorToCar : MonoBehaviour
{
    [SerializeField] private GameEvent colorChangedEvent;
    [SerializeField] private Image materialImg;
    [SerializeField] private Button acceptColorBtn;

    private void Awake()
    {
        acceptColorBtn.onClick.AddListener(SetNewColor);

        if (materialImg.material == null)
        {
            materialImg.material = new Material(Shader.Find("Custom/FCP_Gradient"));
            Debug.Log("—оздан новый материал дл€ materialImg");
        }
    }

    private void SetNewColor()
    {
        if (materialImg != null && materialImg.material != null)
        {
            Material newCarMaterial = new Material(materialImg.material);
            colorChangedEvent.Raise(newCarMaterial);
        }
        else
        {
            Debug.LogError("materialImg или materialImg.material не инициализированы!");
        }
    }
}
