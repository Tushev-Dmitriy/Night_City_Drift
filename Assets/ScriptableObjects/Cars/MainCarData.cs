using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCar", menuName = "Car")]
public class MainCarData : ScriptableObject
{
    public GameObject carObject;
    public GameObject carCameraObject;
    public string carName;
    public int carPrice;
    public CarCharacteristics carCharacteristics;
    public CarView carView;
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