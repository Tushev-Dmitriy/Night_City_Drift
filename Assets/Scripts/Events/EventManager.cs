using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Header("UI objects")]
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject inGameCanvas;

    [Header("Garage UI")]
    [SerializeField] TMP_Text moneyCountText;

    public TMP_Text CheckSpeedText() => speedText;
    public TMP_Text CheckDriftText() => driftText;
    public List<MainCarData> GetCarsData() => _carsData;
    public MainCarData GetCurrentCar() => currentCarData;

    private MainCarData currentCarData;
    private int currentCarIndex = -1;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (_carsData.Count > 0 && currentCarData == null)
        {
            OnCarSelected(true);
        }
    }

    public void OnCarSelected(bool isIncrease)
    {
        if (isIncrease)
        {
            currentCarIndex++;
        } else if (currentCarIndex > 0)
        {
            currentCarIndex--;
        } else
        {
            Debug.LogError("Индекс меньше 0");
        }

        currentCarData = _carsData[currentCarIndex];
        SetupMoneyText();
        engineMenuController.SetupCarData(currentCarData, _userData);
        carPodiumCotroller.SpawnCar(currentCarData);
    }

    public void OnBodyColorChanged(Material newMaterial)
    {
        if (currentCarData != null)
        {
            currentCarData.carView.carColor.color = newMaterial.GetColor("_Color1");
            carPodiumCotroller.SpawnCar(currentCarData);
        }
    }

    public void OnWheelColorChanged(Material newMaterial)
    {
        if (currentCarData != null)
        {
            currentCarData.carView.wheelColor.color = newMaterial.GetColor("_Color1");
            carPodiumCotroller.SpawnCar(currentCarData);
        }
    }

    public void OnPurchaseItem(int price)
    {
        if (currentCarData != null)
        {
            if (_userData.CanBuy(price))
            {
                _userData.BuyItem(price);
                SetupMoneyText();

            }
            else
            {
                Debug.LogError("Недостаточно денег");
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
                Debug.LogError("Недостаточно денег");
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
}