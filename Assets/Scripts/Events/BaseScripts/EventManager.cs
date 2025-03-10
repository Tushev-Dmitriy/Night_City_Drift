using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    [Header("Cars data")]
    [SerializeField] List<MainCarData> _carsData;

    [Header("User data")]
    [SerializeField] UserData _userData;

    [Header("Scene objects")]
    [SerializeField] CarPodiumCotroller carPodiumCotroller;
    [SerializeField] EngineMenuController engineMenuController;
    [SerializeField] SettingsController settingsController;
    [SerializeField] GameObject carPodium;

    [Header("In game UI elements")]
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Text driftText;
    [SerializeField] TMP_Text moneyCountTextInGame;
    [SerializeField] GameObject startGameBtn;
    [SerializeField] GameObject buyCarBtn;
    [SerializeField] GameObject carStatsPanel;

    [Header("UI objects")]
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject inGameCanvas;

    [Header("Garage UI")]
    [SerializeField] TMP_Text moneyCountText;
    [SerializeField] Animator moneyErrorAnimator;

    public TMP_Text CheckSpeedText() => speedText;
    public TMP_Text CheckDriftText() => driftText;
    public List<MainCarData> GetCarsData() => _carsData;
    public MainCarData GetCurrentCar() => currentCarData;

    private MainCarData currentCarData;
    private int currentCarIndex = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (_carsData != null && _carsData.Count > 0)
        {
            currentCarIndex = 0;
            OnCarSelected(false);
        }
    }

    public void OnCarSelected(bool isIncrease)
    {
        if (_carsData == null || _carsData.Count == 0)
        {
            Debug.LogError("Список машин пуст");
            return;
        }

        if (isIncrease)
        {
            currentCarIndex = Mathf.Min(currentCarIndex + 1, _carsData.Count - 1);
        }
        else
        {
            currentCarIndex = Mathf.Max(currentCarIndex - 1, 0);
        }

        currentCarData = _carsData[currentCarIndex];

        if (_userData.userCarsName.Contains(currentCarData.carName))
        {
            SetBtnToCarAction(true);
        }
        else
        {
            SetBtnToCarAction(false);
        }

        SetupMoneyText();
        engineMenuController.SetupCarData(currentCarData, _userData);
        carPodiumCotroller.SpawnCar(currentCarData);
    }

    public void OnBodyColorChanged(Material newMaterial)
    {
        if (currentCarData != null && _userData.CanBuy(5000))
        {
            OnPurchaseItem(5000, false);
            currentCarData.carView.carColor.color = newMaterial.GetColor("_Color1");
            carPodiumCotroller.SpawnCar(currentCarData);
        } else {
            moneyErrorAnimator.SetTrigger("MoneyError");
        }
    }

    public void OnWheelColorChanged(Material newMaterial)
    {
        if (currentCarData != null && _userData.CanBuy(5000))
        {
            OnPurchaseItem(5000, false);
            currentCarData.carView.wheelColor.color = newMaterial.GetColor("_Color1");
            carPodiumCotroller.SpawnCar(currentCarData);
        } else
        {
            moneyErrorAnimator.SetTrigger("MoneyError");
        }
    }

    public void OnPurchaseItem(int price, bool isBuyCar)
    {
        if (currentCarData != null)
        {
            if (_userData.CanBuy(price))
            {
                _userData.BuyItem(price);
                SetupMoneyText();

                if (isBuyCar) AddCarToUser();
            }
            else
            {
                moneyErrorAnimator.SetTrigger("MoneyError");
            }
        }
    }

    public void OnCarPlateChange(string text)
    {
        if (currentCarData != null)
        {
            if (_userData.CanBuy(10000))
            {
                currentCarData.SetCarPlate(text);

            }
            else
            {
                moneyErrorAnimator.SetTrigger("MoneyError");
            }
        }
    }
    private void SetupMoneyText()
    {
        moneyCountText.text = _userData.moneyCount.ToString() + " ₽";
        moneyCountTextInGame.text = _userData.moneyCount.ToString() + " ₽";
    }

    public void StartGameFromUI()
    {
        StartCoroutine(StartGameFromUICor());
    }

    IEnumerator StartGameFromUICor()
    {
        settingsController.SetupSound(_userData);

        AsyncOperation gameScene = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        while (!gameScene.isDone)
        {
            yield return null;
        }

        carPodium.SetActive(false);
        startCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
    }

    public void SetupGarage()
    {
        startCanvas.SetActive(true);
        inGameCanvas.SetActive(false);
        carPodium.SetActive(true);
    }

    private void SetBtnToCarAction(bool haveCar)
    {
        if (haveCar)
        {
            startGameBtn.gameObject.SetActive(true);
            buyCarBtn.gameObject.SetActive(false);
            carStatsPanel.SetActive(true);
        } else
        {
            startGameBtn.gameObject.SetActive(false);
            buyCarBtn.gameObject.SetActive(true);
            carStatsPanel.SetActive(false);
            buyCarBtn.transform.GetChild(0).GetComponent<TMP_Text>().text = currentCarData.carPrice.ToString() + " ₽";
            buyCarBtn.GetComponent<Button>().onClick.RemoveAllListeners();
            buyCarBtn.GetComponent<Button>().onClick.AddListener(delegate { OnPurchaseItem(currentCarData.carPrice, true); });
        }
    }

    private void AddCarToUser()
    {
        _userData.userCarsName.Add(currentCarData.carName);
        SetBtnToCarAction(true);
        SaveManager.Instance?.SaveUserData();
    }

    public void MoneyForYG(int money)
    {
        OnPurchaseItem(money, false);
    }
}