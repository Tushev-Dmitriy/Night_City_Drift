using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EngineMenuController : MonoBehaviour
{
    //TODO stats data, stats setup, mainCarData another lvls
    [Header("Engine")]
    [SerializeField] TMP_Text engineStatText;
    [SerializeField] Button enginePurchaseBtn;
    [SerializeField] TMP_Text enginePriceText;

    [Header("Brake")]
    [SerializeField] TMP_Text brakeStatText;
    [SerializeField] Button brakePurchaseBtn;
    [SerializeField] TMP_Text brakePriceText;

    [Header("Wheel")]
    [SerializeField] TMP_Text wheelStatText;
    [SerializeField] Button wheelPurchaseBtn;
    [SerializeField] TMP_Text wheelPriceText;

    [Header("Turbine")]
    [SerializeField] TMP_Text turbineStatText;
    [SerializeField] Button turbinePurchaseBtn;
    [SerializeField] TMP_Text turbinePriceText;

    [Header("Nitro")]
    [SerializeField] TMP_Text nitroStatText;
    [SerializeField] Button nitroPurchaseBtn;
    [SerializeField] TMP_Text nitroPriceText;

    private MainCarData _currentCarData;

    public void SetupCarData(MainCarData carData)
    {
        _currentCarData = carData;
        SetStatsData();
    }

    private void SetStatsData()
    {
        engineStatText.text = _currentCarData.carCharacteristics.engineLvl.ToString();
        brakeStatText.text = _currentCarData.carCharacteristics.brakeLvl.ToString();
        wheelStatText.text = _currentCarData.carCharacteristics.steeringAngleLvl.ToString();
    }

    private void SetActionToBtn()
    {

    }
}
