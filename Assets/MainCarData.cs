using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCar", menuName = "Car")]
public class MainCarData : ScriptableObject
{
    //���������� ��������� ���� ������� ����� �������
    //����� ���� ������ � ����� ������ 
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
    Hatchback, //����
    Saloon, //�����
    Coupe, //����
    Convertible, //���������
    Liftback, //�������
}