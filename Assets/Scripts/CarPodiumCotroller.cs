using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPodiumCotroller : MonoBehaviour
{
    [SerializeField] GameObject carModelPos;

    private void Update()
    {
        RotateCarModelPos();
    }

    private void RotateCarModelPos()
    {
        carModelPos.transform.Rotate(0, 0.05f, 0);
    }
}
