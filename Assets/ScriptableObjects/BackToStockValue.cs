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
                    car.carView.carPlate = "À053ÂÑ178";
                    break;
                case "BMW M3 E30":
                    car.carCharacteristics.maxSpeed = 170;
                    car.carCharacteristics.engineLvl = 8;
                    car.carCharacteristics.steeringAngleLvl = 30;
                    car.carCharacteristics.brakeLvl = 500;
                    car.carCharacteristics.haveTurbine = false;
                    car.carView.carPlate = "À053ÂÑ178";
                    break;
                case "Ford Mustang":
                    car.carCharacteristics.maxSpeed = 160;
                    car.carCharacteristics.engineLvl = 7;
                    car.carCharacteristics.steeringAngleLvl = 30;
                    car.carCharacteristics.brakeLvl = 300;
                    car.carCharacteristics.haveTurbine = false;
                    car.carView.carPlate = "Â982ÀÎ781";
                    break;
                case "Mazda RX7":
                    car.carCharacteristics.maxSpeed = 150;
                    car.carCharacteristics.engineLvl = 6;
                    car.carCharacteristics.steeringAngleLvl = 40;
                    car.carCharacteristics.brakeLvl = 300;
                    car.carCharacteristics.haveTurbine = false;
                    car.carView.carPlate = "À761ÊÅ702";
                    break;
                case "Nissan Skyline R34":
                    car.carCharacteristics.maxSpeed = 170;
                    car.carCharacteristics.engineLvl = 8;
                    car.carCharacteristics.steeringAngleLvl = 35;
                    car.carCharacteristics.brakeLvl = 500;
                    car.carCharacteristics.haveTurbine = false;
                    car.carView.carPlate = "Õ726ÀÓ052";
                    break;
                case "Toyota Supra":
                    car.carCharacteristics.maxSpeed = 170;
                    car.carCharacteristics.engineLvl = 8;
                    car.carCharacteristics.steeringAngleLvl = 45;
                    car.carCharacteristics.brakeLvl = 400;
                    car.carCharacteristics.haveTurbine = false;
                    car.carView.carPlate = "Î056ÐÂ156";
                    break;
                case "Audi R8":
                    car.carCharacteristics.maxSpeed = 180;
                    car.carCharacteristics.engineLvl = 9;
                    car.carCharacteristics.steeringAngleLvl = 40;
                    car.carCharacteristics.brakeLvl = 600;
                    car.carCharacteristics.haveTurbine = false;
                    car.carView.carPlate = "Â845ÌÕ692";
                    break;
                case "BMW M5 F90":
                    car.carCharacteristics.maxSpeed = 190;
                    car.carCharacteristics.engineLvl = 10;
                    car.carCharacteristics.steeringAngleLvl = 40;
                    car.carCharacteristics.brakeLvl = 600;
                    car.carCharacteristics.haveTurbine = false;
                    car.carView.carPlate = "À053ÂÑ178";
                    break;
            }

            SaveManager.Instance?.SaveData(car.carName);
        }
    }
}
