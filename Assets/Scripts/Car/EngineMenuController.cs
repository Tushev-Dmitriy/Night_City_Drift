using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EngineMenuController : MonoBehaviour
{
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

    [Header("Events")]
    [SerializeField] private GameEvent purchaseItemEvent;

    private MainCarData _currentCarData;
    private UserData _userData;

    private int _maxEngineLvl = 10;
    private int _maxBrakeLvl = 600;
    private int _maxWheelLvl = 45;

    public void SetupCarData(MainCarData carData, UserData userData)
    {
        _currentCarData = carData;
        _userData = userData;
        SetupMenuData();
    }

    private void SetupMenuData()
    {
        SetStatsData();
        SetPriceToStats();
        SetActionsToBtns();
    }

    private void SetStatsData()
    {
        engineStatText.text = _currentCarData.carCharacteristics.engineLvl.ToString();
        brakeStatText.text = ((_currentCarData.carCharacteristics.brakeLvl/100)+4).ToString();
        wheelStatText.text = ((_currentCarData.carCharacteristics.steeringAngleLvl/5)+1).ToString();
        turbineStatText.text = _currentCarData.carCharacteristics.haveTurbine ? "1" : "0";
        nitroStatText.text = _currentCarData.carCharacteristics.haveNitro ? "1" : "0";
    }

    private void SetPriceToStats()
    {
        enginePriceText.text = (_currentCarData.carCharacteristics.engineLvl * 1000).ToString() + " ₽";
        brakePriceText.text = ((_currentCarData.carCharacteristics.brakeLvl / 100) * 1000).ToString() + " ₽";
        wheelPriceText.text = ((_currentCarData.carCharacteristics.steeringAngleLvl / 5) * 1000).ToString() + " ₽";
        turbinePriceText.text = "10000 ₽";
        nitroPriceText.text = "10000 ₽";
    }

    private void SetActionsToBtns()
    {
        enginePurchaseBtn.onClick.RemoveAllListeners();
        brakePurchaseBtn.onClick.RemoveAllListeners();
        wheelPurchaseBtn.onClick.RemoveAllListeners();
        turbinePurchaseBtn.onClick.RemoveAllListeners();
        nitroPurchaseBtn.onClick.RemoveAllListeners();

        enginePurchaseBtn.onClick.AddListener(delegate { PurchaseItem(0); });
        brakePurchaseBtn.onClick.AddListener(delegate { PurchaseItem(1); });
        wheelPurchaseBtn.onClick.AddListener(delegate { PurchaseItem(2); });
        turbinePurchaseBtn.onClick.AddListener(delegate { PurchaseItem(3); });
        nitroPurchaseBtn.onClick.AddListener(delegate { PurchaseItem(4); });
    }

    private void PurchaseItem(int numOfStat)
    {
        switch (numOfStat)
        {
            case 0: //engine
                if (_userData.CanBuy(_currentCarData.carCharacteristics.engineLvl * 1000) && CanUpgradeStat(0))
                {
                    purchaseItemEvent.Raise(_currentCarData.carCharacteristics.engineLvl * 1000);
                    _currentCarData.UpgradeStats(0);
                }
                SetupMenuData();
                break;
            case 1: //brake
                if (_userData.CanBuy((_currentCarData.carCharacteristics.brakeLvl / 100) * 1000) && CanUpgradeStat(1))
                {
                    purchaseItemEvent.Raise((_currentCarData.carCharacteristics.brakeLvl / 100) * 1000);
                    _currentCarData.UpgradeStats(2);
                }
                SetupMenuData();
                break;
            case 2: //angle
                if (_userData.CanBuy((_currentCarData.carCharacteristics.steeringAngleLvl / 5) * 1000) && CanUpgradeStat(2))
                {
                    purchaseItemEvent.Raise((_currentCarData.carCharacteristics.steeringAngleLvl / 5) * 1000);
                    _currentCarData.UpgradeStats(1);
                }
                SetupMenuData();
                break;
            case 3: //turbine
                if (_userData.CanBuy(10000) && CanUpgradeStat(3))
                {
                    Debug.Log(CanUpgradeStat(3));
                    purchaseItemEvent.Raise(10000);
                    _currentCarData.UpgradeStats(3);
                }
                SetupMenuData();
                break;
            case 4: //nitro
                if (_userData.CanBuy(10000) && CanUpgradeStat(4))
                {
                    purchaseItemEvent.Raise(10000);
                    _currentCarData.UpgradeStats(4);
                }
                SetupMenuData();
                break;
        }
    }

    private bool CanUpgradeStat(int numOfStat)
    {
        switch (numOfStat)
        {
            case 0: //engine
                return _currentCarData.carCharacteristics.engineLvl < _maxEngineLvl;
            case 1: //brake
                return _currentCarData.carCharacteristics.brakeLvl < _maxBrakeLvl;
            case 2: //angle
                return _currentCarData.carCharacteristics.steeringAngleLvl < _maxWheelLvl;
            case 3: //turbine
                return !_currentCarData.carCharacteristics.haveTurbine;
            case 4: //nitro
                return !_currentCarData.carCharacteristics.haveNitro;
        }

        return false;
    }


}
