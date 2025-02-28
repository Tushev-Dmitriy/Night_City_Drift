using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    [Header("Cars data")]
    [SerializeField] List<MainCarData> _carsData;

    [Header("Scene objects")]
    [SerializeField] CarPodiumCotroller carPodiumCotroller;
    [SerializeField] EngineMenuController engineMenuController;
    [SerializeField] GameObject carPodium;

    [Header("UI elements")]
    [SerializeField] TMP_Text speedText;
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject inGameCanvas;

    public TMP_Text CheckSpeedText() => speedText;
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
        engineMenuController.SetupCarData(currentCarData);
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

    public void StartGameFromUI()
    {
        StartCoroutine(StartGameFromUICor());
    }

    IEnumerator StartGameFromUICor()
    {
        AsyncOperation gameScene = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        while (!gameScene.isDone)
        {
            yield return null;
        }

        carPodium.SetActive(false);
        startCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
    }
}