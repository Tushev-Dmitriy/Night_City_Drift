using UnityEngine;
using System.Collections.Generic;
using YG;
using System.Collections;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    [SerializeField] private List<MainCarData> saveableCars = new List<MainCarData>();
    [SerializeField] private UserData userData;

    private string localUserDataKey = "UserDataSave";
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadLocalData();
        LoadLocalUserData();
        //LoadAllData();
    }

    public void SaveData(string carName)
    {
        MainCarData carToSave = saveableCars.Find(car => car.carName == carName);
        if (carToSave != null)
        {
            CarSaveData saveData = new CarSaveData
            {
                carName = carToSave.carName,
                maxSpeed = carToSave.carCharacteristics.maxSpeed,
                engineLvl = carToSave.carCharacteristics.engineLvl,
                steeringAngleLvl = carToSave.carCharacteristics.steeringAngleLvl,
                brakeLvl = carToSave.carCharacteristics.brakeLvl,
                haveNitro = carToSave.carCharacteristics.haveNitro,
                haveTurbine = carToSave.carCharacteristics.haveTurbine,
                carPlate = carToSave.carView.carPlate
            };
            string json = JsonUtility.ToJson(saveData);
            SaveToLocal(carToSave.carName, json);
        }
    }

    private void SaveToLocal(string carName, string json)
    {
        PlayerPrefs.SetString($"CarSaveData_{carName}", json);
        PlayerPrefs.Save();
        Debug.Log($"ƒанные сохранены локально дл€ {carName}");
    }

    private void LoadLocalData()
    {
        foreach (var car in saveableCars)
        {
            string key = $"CarSaveData_{car.carName}";
            if (PlayerPrefs.HasKey(key))
            {
                string json = PlayerPrefs.GetString(key);
                ApplySaveData(car, json);
                Debug.Log($"ƒанные загружены локально дл€ {car.carName}");
            }
        }
    }

    private void ApplySaveData(MainCarData car, string json)
    {
        CarSaveData saveData = JsonUtility.FromJson<CarSaveData>(json);
        car.carName = saveData.carName;
        car.carCharacteristics.maxSpeed = saveData.maxSpeed;
        car.carCharacteristics.engineLvl = saveData.engineLvl;
        car.carCharacteristics.steeringAngleLvl = saveData.steeringAngleLvl;
        car.carCharacteristics.brakeLvl = saveData.brakeLvl;
        car.carCharacteristics.haveNitro = saveData.haveNitro;
        car.carCharacteristics.haveTurbine = saveData.haveTurbine;
        car.carView.carPlate = saveData.carPlate;
    }

    public void SaveUserData()
    {
        if (userData != null)
        {
            string json = JsonUtility.ToJson(userData);
            PlayerPrefs.SetString(localUserDataKey, json);
            PlayerPrefs.Save();
            Debug.Log("UserData сохранены локально");
        }
    }

    private void LoadLocalUserData()
    {
        if (PlayerPrefs.HasKey(localUserDataKey))
        {
            string json = PlayerPrefs.GetString(localUserDataKey);
            JsonUtility.FromJsonOverwrite(json, userData);
            Debug.Log(userData.userCars.Count);
            Debug.Log(userData.userCars[0]);
            Debug.Log("UserData загружены локально");
        }
    }

    public void LoadAllDataYG()
    {
        userData.moneyCount = YG2.saves.moneyCount;
        userData.carVolume = YG2.saves.carVolume;
        userData.musicVolume = YG2.saves.musicVolume;

        foreach (var savedCar in YG2.saves.cars)
        {
            MainCarData car = saveableCars.Find(c => c.carName == savedCar.carName);
            if (car != null)
            {
                car.carCharacteristics.maxSpeed = savedCar.maxSpeed;
                car.carCharacteristics.engineLvl = savedCar.engineLvl;
                car.carCharacteristics.steeringAngleLvl = savedCar.steeringAngleLvl;
                car.carCharacteristics.brakeLvl = savedCar.brakeLvl;
                car.carCharacteristics.haveNitro = savedCar.haveNitro;
                car.carCharacteristics.haveTurbine = savedCar.haveTurbine;
                car.carView.carPlate = savedCar.carPlate;
            }
        }
    }

    public void SaveAllDataYG()
    {
        YG2.saves.moneyCount = userData.moneyCount;
        YG2.saves.carVolume = userData.carVolume;
        YG2.saves.musicVolume = userData.musicVolume;

        YG2.saves.cars.Clear();
        foreach (var car in saveableCars)
        {
            CarSaveData saveData = new CarSaveData
            {
                carName = car.carName,
                maxSpeed = car.carCharacteristics.maxSpeed,
                engineLvl = car.carCharacteristics.engineLvl,
                steeringAngleLvl = car.carCharacteristics.steeringAngleLvl,
                brakeLvl = car.carCharacteristics.brakeLvl,
                haveNitro = car.carCharacteristics.haveNitro,
                haveTurbine = car.carCharacteristics.haveTurbine,
                carPlate = car.carView.carPlate
            };
            YG2.saves.cars.Add(saveData);
        }

        YG2.SaveProgress();
    }
}