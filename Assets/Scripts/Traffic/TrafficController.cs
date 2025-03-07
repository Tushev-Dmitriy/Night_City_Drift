using UnityEngine;
using Dreamteck.Splines;
using System.Collections;

public class TrafficController : MonoBehaviour
{
    [SerializeField] private SplineComputer citySpline;
    [SerializeField] private SplineComputer roadSpline;
    [SerializeField] private SplineComputer roadSplineReverse;
    [SerializeField] private Transform agentsContainer;
    [SerializeField] private GameObject[] carPrefabs;
    [SerializeField] private float speed; // Скорость для города
    [SerializeField] private float speedHighway; // Скорость для хайвея
    [SerializeField] private int initialCarCount; // Количество машин для города
    [SerializeField] private int initialCarCountHighway; // Количество машин для хайвея

    private void Start()
    {
        citySpline.sampleMode = SplineComputer.SampleMode.Uniform;
        roadSpline.sampleMode = SplineComputer.SampleMode.Uniform;
        roadSplineReverse.sampleMode = SplineComputer.SampleMode.Uniform;

        StartCoroutine(SpawnCarsWithDelay());
    }

    private IEnumerator SpawnCarsWithDelay()
    {
        for (int i = 0; i < initialCarCount; i++)
        {
            double t = (double)i / (initialCarCount - 1);
            SpawnCar(t, 0);
            yield return null;
        }

        for (int i = 0; i < initialCarCountHighway; i++)
        {
            double t = (double)i / (initialCarCountHighway - 1);
            SpawnCar(t, 1);
            yield return null;
        }

        for (int i = 0; i < initialCarCountHighway; i++)
        {
            double t = (double)i / (initialCarCountHighway - 1);
            SpawnCar(t, 2);
            yield return null;
        }
    }

    void SpawnCar(double t, int splineIndex)
    {
        SplineComputer spline = null;

        switch (splineIndex)
        {
            case 0:
                spline = citySpline;
                break;
            case 1:
                spline = roadSpline;
                break;
            case 2:
                spline = roadSplineReverse;
                break;
            default:
                Debug.LogError("Неверный индекс сплайна!");
                return;
        }

        GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];
        GameObject car = Instantiate(carPrefab, agentsContainer);

        SplineFollower follower = car.GetComponent<SplineFollower>();
        if (follower == null)
        {
            follower = car.AddComponent<SplineFollower>();
        }

        follower.spline = spline;
        follower.follow = false;

        // Устанавливаем скорость
        switch (splineIndex)
        {
            case 0:
                follower.followSpeed = speed;
                Debug.Log($"City speed set to: {speed}");
                break;
            case 1:
                follower.followSpeed = speedHighway;
                Debug.Log($"Highway speed set to: {speedHighway} for roadSpline");
                break;
            case 2:
                follower.followSpeed = speedHighway;
                Debug.Log($"Highway speed set to: {speedHighway} for roadSplineReverse");
                break;
        }

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

        // Проверяем скорость после активации
        Debug.Log($"Actual speed after activation for {car.name}: {follower.followSpeed}");

        Vector3 postActivationPosition = car.transform.position;

        SplineComputer spline = null;
        switch (splineIndex)
        {
            case 0:
                spline = citySpline;
                break;
            case 1:
                spline = roadSpline;
                break;
            case 2:
                spline = roadSplineReverse;
                break;
        }

        if (spline != null && Vector3.Distance(postActivationPosition, spline.EvaluatePosition(t)) > 0.1f)
        {
            Debug.LogWarning($"Позиция после активации не совпадает! t = {t}, сплайн = {spline.name}, позиция = {postActivationPosition}");
        }
    }
}