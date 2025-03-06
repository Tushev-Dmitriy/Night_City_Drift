using Dreamteck.Splines;
using UnityEngine;
using System.Collections.Generic;

public class TrafficCarController : MonoBehaviour
{
    [SerializeField] private SplineFollower splineFollower;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float accelerationTime;
    [SerializeField] private float decelerationTime;
    [SerializeField] private float safeDistance;
    [SerializeField] private float minStopDistance;

    public int numOfRoad;

    private float currentSpeed = 0.0f;
    private Collider triggerCollider;
    private HashSet<Collider> currentColliders = new HashSet<Collider>();

    private void Start()
    {
        triggerCollider = GetComponent<Collider>();
        splineFollower.followSpeed = currentSpeed;

        if (numOfRoad == 0)
        {
            maxSpeed = 10.0f;
        }
        else
        {
            maxSpeed = 30.0f;
            safeDistance = 100.0f;
            minStopDistance = 30.0f;
        }
    }

    private void Update()
    {
        UpdateSpeed();
        splineFollower.followSpeed = currentSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "city_part_collider")
        {
            currentColliders.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name != "city_part_collider")
        {
            currentColliders.Remove(other);
        }
    }

    private void UpdateSpeed()
    {
        float desiredSpeed = maxSpeed;
        bool obstaclePresent = false;
        float minDistance = float.MaxValue;

        foreach (var collider in currentColliders)
        {
            if (collider == null) continue;
            TrafficCarController leadingCar = collider.GetComponent<TrafficCarController>();
            if (leadingCar != null && leadingCar != this)
            {
                float distance = CalculateDistanceToCar(leadingCar);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }
            else
            {
                obstaclePresent = true;
                break;
            }
        }

        if (obstaclePresent)
        {
            desiredSpeed = 0.0f;
        }
        else if (minDistance < minStopDistance)
        {
            desiredSpeed = 0.0f; 
        }
        else if (minDistance < safeDistance)
        {
            desiredSpeed = maxSpeed * (minDistance / safeDistance);
        }
        else
        {
            desiredSpeed = maxSpeed;
        }

        float rate = (desiredSpeed > currentSpeed) ? (maxSpeed / accelerationTime) : (maxSpeed / decelerationTime);
        float deltaSpeed = rate * Time.deltaTime;
        if (desiredSpeed > currentSpeed)
        {
            currentSpeed = Mathf.Min(currentSpeed + deltaSpeed, desiredSpeed);
        }
        else
        {
            currentSpeed = Mathf.Max(currentSpeed - deltaSpeed, desiredSpeed);
        }
    }

    private float CalculateDistanceToCar(TrafficCarController otherCar)
    {
        if (otherCar == null || otherCar.splineFollower == null) return float.MaxValue;

        float myPercent = (float)splineFollower.GetPercent();
        float otherPercent = (float)otherCar.splineFollower.GetPercent();
        float splineLength = splineFollower.spline.CalculateLength();

        float deltaPercent = otherPercent - myPercent;
        if (deltaPercent < 0.0f) deltaPercent += 1.0f;
        return deltaPercent * splineLength;
    }
}