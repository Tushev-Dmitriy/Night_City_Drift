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
        brakeStatText.text = (_currentCarData.carCharacteristics.brakeLvl/100).ToString();
        wheelStatText.text = (_currentCarData.carCharacteristics.steeringAngleLvl/5).ToString();
        turbineStatText.text = _currentCarData.carCharacteristics.haveTurbine ? "1" : "0";
        turbineStatText.text = _currentCarData.carCharacteristics.haveNitro ? "1" : "0";
    }

    private void SetPriceToStats()
    {
        enginePriceText.text = (_currentCarData.carCharacteristics.engineLvl * 1000).ToString() + " ₽";
        brakePriceText.text = ((_currentCarData.carCharacteristics.brakeLvl / 100) * 1000).ToString() + " ₽";
        wheelPriceText.text = ((_currentCarData.carCharacteristics.steeringAngleLvl / 5) * 1000).ToString() + " ₽";
        turbinePriceText.text = _currentCarData.carCharacteristics.haveTurbine ? "0" : "10000 ₽";
        nitroPriceText.text = _currentCarData.carCharacteristics.haveNitro ? "0" : "10000 ₽";
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
        int tempPrice;
        switch (numOfStat)
        {
            case 0: //engine
                tempPrice = _userData.moneyCount - (_currentCarData.carCharacteristics.engineLvl * 1000);
                if (tempPrice >= 0)
                {
                    _userData.BuyItem(_currentCarData.carCharacteristics.engineLvl * 1000);
                    _currentCarData.UpgradeStats(0);
                } else
                {
                    Debug.LogError("Недостаточно денег");
                }
                SetupMenuData();
                break;
            case 1: //brake
                tempPrice = _userData.moneyCount - ((_currentCarData.carCharacteristics.brakeLvl / 100) * 1000);
                if (tempPrice >= 0)
                {
                    _userData.BuyItem((_currentCarData.carCharacteristics.brakeLvl / 100) * 1000);
                    _currentCarData.UpgradeStats(2);
                }
                else
                {
                    Debug.LogError("Недостаточно денег");
                }
                SetupMenuData();
                break;
            case 2: //angle
                tempPrice = _userData.moneyCount - ((_currentCarData.carCharacteristics.steeringAngleLvl / 5) * 1000);
                if (tempPrice >= 0)
                {
                    _userData.BuyItem((_currentCarData.carCharacteristics.steeringAngleLvl / 5) * 1000);
                    _currentCarData.UpgradeStats(1);

                }
                else
                {
                    Debug.LogError("Недостаточно денег");
                }
                SetupMenuData();
                break;
            case 3: //turbine
                tempPrice = _userData.moneyCount - 10000;
                if (tempPrice >= 0)
                {
                    _userData.BuyItem(10000);
                    _currentCarData.UpgradeStats(3);
                }
                else
                {
                    Debug.LogError("Недостаточно денег");
                }
                SetupMenuData();
                break;
            case 4: //nitro
                tempPrice = _userData.moneyCount - 10000;
                if (tempPrice >= 0)
                {
                    _userData.BuyItem(10000);
                    _currentCarData.UpgradeStats(4);
                }
                else
                {
                    Debug.LogError("Недостаточно денег");
                }
                SetupMenuData();
                break;
        }
    }
}
