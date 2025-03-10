using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToStockValue : MonoBehaviour
{
    [SerializeField] List<MainCarData> carData;

    void Start()
    {
        foreach (var car in carData)
        {
            switch (car.carName)
            {
                case "Vaz 2107":
                    car.carCharacteristics.maxSpeed = 140;
                    car.carCharacteristics.engineLvl = 5;
                    car.carCharacteristics.steeringAngleLvl = 25;
                    car.carCharacteristics.brakeLvl = 300;
                    car.carCharacteristics.haveTurbine = false;
                    car.carView.carPlate = "ю053бя178";
                    break;
                case "BMW M3 E30":
                    car.carCharacteristics.maxSpeed = 170;
                    car.carCharacteristics.engineLvl = 8;
                    car.carCharacteristics.steeringAngleLvl = 30;
                    car.carCharacteristics.brakeLvl = 500;
                    car.carCharacteristics.haveTurbine = false;
                    car.carView.carPlate = "ю053бя178";
                    break;
            }

            SaveManager.Instance?.SaveData(car.carName);
        }
    }
}
