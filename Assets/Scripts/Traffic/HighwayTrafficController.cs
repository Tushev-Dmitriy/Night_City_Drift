using UnityEngine;
using Dreamteck.Splines;
using System.Collections;
using Unity.VisualScripting;
using static UnityEngine.AudioSettings;

public class HighwayTrafficController : MonoBehaviour
{
    [SerializeField] private SplineComputer roadSpline; // Сплайн для одной стороны хайвея
    [SerializeField] private SplineComputer roadSplineReverse; // Сплайн для другой стороны хайвея (обратное направление)
    [SerializeField] private Transform agentsContainer; // Контейнер для машин
    [SerializeField] private GameObject[] carPrefabs; // Префабы машин
    [SerializeField] private float speedHighway; // Скорость движения машин на хайвее
    [SerializeField] private int initialCarCountHighway; // Количество машин на хайвее

    public void StartSpawn(bool isMobile)
    {
        if (isMobile)
        {
            initialCarCountHighway /= 3;
        }

        roadSpline.sampleMode = SplineComputer.SampleMode.Uniform;
        roadSplineReverse.sampleMode = SplineComputer.SampleMode.Uniform;

        StartCoroutine(SpawnCarsWithDelay());
    }

    private IEnumerator SpawnCarsWithDelay()
    {
        for (int i = 0; i < initialCarCountHighway; i++)
        {
            double t = (double)i / (initialCarCountHighway - 1);
            SpawnCar(t, 0); // roadSpline
            yield return null;
        }

        for (int i = 0; i < initialCarCountHighway; i++)
        {
            double t = (double)i / (initialCarCountHighway - 1);
            SpawnCar(t, 1); // roadSplineReverse
            yield return null;
        }
    }

    void SpawnCar(double t, int splineIndex)
    {
        SplineComputer spline = null;

        switch (splineIndex)
        {
            case 0:
                spline = roadSpline;
                break;
            case 1:
                spline = roadSplineReverse;
                break;
            default:
                Debug.LogError("Неверный индекс сплайна!");
                return;
        }

        GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];
        GameObject car = Instantiate(carPrefab, agentsContainer);

        SplineFollower follower = car.GetOrAddComponent<SplineFollower>();
        TrafficCarController trafficCarController = car.GetComponent<TrafficCarController>();
        trafficCarController.numOfRoad = 1;

        follower.spline = spline;
        follower.follow = false;
        follower.followSpeed = speedHighway;
        follower.wrapMode = SplineFollower.Wrap.Loop;

        Vector3 position = spline.EvaluatePosition(t);
        car.transform.position = position;

        follower.SetPercent(t);
        follower.Rebuild();

        StartCoroutine(EnableFollow(car, follower, t, splineIndex));
    }

    private IEnumerator EnableFollow(GameObject car, SplineFollower follower, double t, int splineIndex)
    {
        yield return new WaitForSeconds(0.1f);

        follower.follow = true;
        Vector3 postActivationPosition = car.transform.position;

        SplineComputer spline = null;
        switch (splineIndex)
        {
            case 0:
                spline = roadSpline;
                break;
            case 1:
                spline = roadSplineReverse;
                break;
        }

        if (spline != null && Vector3.Distance(postActivationPosition, spline.EvaluatePosition(t)) > 0.1f)
        {
            Debug.LogWarning($"Позиция после активации не совпадает! t = {t}, сплайн = {spline.name}, позиция = {postActivationPosition}");
        }
    }
}