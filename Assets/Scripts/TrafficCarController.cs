using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficCarController : MonoBehaviour
{
    [SerializeField] SplineFollower splineFollower;

    private float carStockSpeed;
    //private Collider carCollider;

    private void Start()
    {
        //carCollider = GetComponent<Collider>();
        carStockSpeed = splineFollower.followSpeed;
        //StartCoroutine(CheckColliders());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "city_part_collider") return;
        StartCoroutine(SetCarSpeed());
    }

    private void OnTriggerExit(Collider other)
    {
        splineFollower.followSpeed = carStockSpeed;
    }

    IEnumerator SetCarSpeed()
    {
        while (splineFollower.followSpeed > 0)
        {
            splineFollower.followSpeed--;
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    //IEnumerator CheckColliders()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(5);
    //        if (carCollider.)
    //    }
    //}
}
