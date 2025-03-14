using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPodiumCotroller : MonoBehaviour
{
    [SerializeField] EventManager eventManager;
    [SerializeField] GameObject carModelPos;

    private GameObject currentCarModel;

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            RotateCarModelPos();
        }
    }

    private void RotateCarModelPos()
    {
        carModelPos.transform.Rotate(0, 0.05f, 0);
    }
    
    public void SpawnCar(MainCarData carData)
    {
        if (carModelPos.transform.childCount > 0)
        {
            Destroy(carModelPos.transform.GetChild(0).gameObject);
        }

        Instantiate(carData.carPodiumObject, carModelPos.transform);
    }
}