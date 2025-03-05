using UnityEngine;
using Dreamteck.Splines;
using Unity.VisualScripting;

public class TrafficController : MonoBehaviour
{
    public SplineComputer spline;
    public Transform agentsContainer;
    public GameObject[] carPrefabs;
    public float spawnInterval = 10f;
    public float speed = 10f;

    private float spawnTimer;
    private int numOfCars = 0;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval && carPrefabs.Length > 0 && spline != null && numOfCars < 15)
        {
            SpawnCar();
            spawnTimer = 0f;
            numOfCars++;
        }
    }

    void SpawnCar()
    {
        GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];
        GameObject car = Instantiate(carPrefab, agentsContainer);

        SplineFollower follower = car.GetOrAddComponent<SplineFollower>();
        follower.spline = spline;
        follower.followSpeed = speed;

        float t = Random.value;
        follower.SetPercent(t);
    }
}