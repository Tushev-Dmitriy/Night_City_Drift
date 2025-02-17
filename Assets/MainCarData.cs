using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCar", menuName = "Car")]
public class MainCarData : ScriptableObject
{
    //максималка двигатель руль тормоза нитро турбина
    //номер цвет кузова и колес колеса 
    public string carName;
    public Sprite carIcon;
    public int carPrice;
}

public class CarCharacteristics
{
    public int maxSpeed;
    public int engineLvl;
    public int steeringAngleLvl;
    public int brakeLvl;
    public bool haveNitro;
    public bool haveTurbine;
}

public class CarView
{
    public string carPlate;
    public Color carColor;
}

public enum TypeOfBody
{
    Hatchback, //хэтч
    Saloon, //седан
    Coupe, //купе
    Convertible, //кабриолет
    Liftback, //лифтбэк
}