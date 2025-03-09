using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewCar", menuName = "Car")]
public class MainCarData : ScriptableObject
{
    public GameObject carObject;
    public GameObject carPodiumObject;
    public string carName;
    public int carPrice;
    public Vector3 cameraOffset;
    public CarCharacteristics carCharacteristics;
    public CarView carView;

    public void UpgradeStats(int numOfStat)
    {
        switch (numOfStat)
        {
            case 0: //engine
                carCharacteristics.maxSpeed += 10;
                carCharacteristics.engineLvl++;
                SaveManager.Instance?.SaveData(carName);
                break;
            case 1: //angle
                carCharacteristics.steeringAngleLvl += 5;
                SaveManager.Instance?.SaveData(carName);
                break;
            case 2: //brake
                carCharacteristics.brakeLvl += 100;
                SaveManager.Instance?.SaveData(carName);
                break;
            case 3: //turbine
                carCharacteristics.haveTurbine = true;
                SaveManager.Instance?.SaveData(carName);
                break;
            case 4: //nitro
                carCharacteristics.haveNitro = true;
                SaveManager.Instance?.SaveData(carName);
                break;
        }
    }

    public void SetCarPlate(string newPlate)
    {
        carView.carPlate = newPlate;
        SaveManager.Instance?.SaveData(carName);
    }
}

[Serializable]
public class CarCharacteristics
{
    public int maxSpeed = 120;
    public int engineLvl = 5;
    public int steeringAngleLvl = 25;
    public int brakeLvl = 300;
    public bool haveNitro = false;
    public bool haveTurbine = false;
}

[Serializable]
public class CarView
{
    public string carPlate = "A123AA";
    public Material carColor;
    public TypeOfBody bodyType;
    public Material wheelColor;
}

[Serializable]
public enum TypeOfBody
{
    Hatchback,
    Saloon,
    Coupe,
    Convertible,
    Liftback
}