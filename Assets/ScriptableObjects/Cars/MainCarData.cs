using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCar", menuName = "Car")]
public class MainCarData : ScriptableObject
{
    public GameObject carObject;
    public GameObject carPodiumObject;
    public string carName;
    public int carPrice;
    public CarCharacteristics carCharacteristics;
    public CarView carView;

    public void UpgradeStats(int numOfStat)
    {
        switch (numOfStat)
        {
            case 0: //engine
                carCharacteristics.maxSpeed = carCharacteristics.maxSpeed + 10;
                carCharacteristics.engineLvl++;
                break;
            case 1: //angle
                carCharacteristics.steeringAngleLvl = carCharacteristics.steeringAngleLvl + 5;
                break;
            case 2: //brake
                carCharacteristics.brakeLvl = carCharacteristics.brakeLvl + 100;
                break;
            case 3: //nitro
                carCharacteristics.haveNitro = true;
                break;
            case 4: //turbine
                carCharacteristics.haveTurbine = true;
                break;
        }
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

public enum TypeOfBody
{
    Hatchback, //хэтч
    Saloon, //седан
    Coupe, //купе
    Convertible, //кабриолет
    Liftback, //лифтбэк
}