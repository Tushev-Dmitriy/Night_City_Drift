using UnityEngine;
using Dreamteck.Splines;
using System.Collections;

public class CityTrafficController : MonoBehaviour
{
    [SerializeField] private SplineComputer citySpline; // ������ ��� ������
    [SerializeField] private Transform agentsContainer; // ��������� ��� �����
    [SerializeField] private GameObject[] carPrefabs; // ������� �����
    [SerializeField] private float speed; // �������� �������� ����� � ������
    [SerializeField] private int initialCarCount; // ���������� ����� � ������

    private void Start()
    {
        citySpline.sampleMode = SplineComputer.SampleMode.Uniform;
        StartCoroutine(SpawnCarsWithDelay());
    }

    private IEnumerator SpawnCarsWithDelay()
    {
        for (int i = 0; i < initialCarCount; i++)
        {
            double t = (double)i / (initialCarCount - 1);
            SpawnCar(t);
            yield return null;
        }
    }

    void SpawnCar(double t)
    {
        GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];
        GameObject car = Instantiate(carPrefab, agentsContainer);

        SplineFollower follower = car.GetComponent<SplineFollower>();
        TrafficCarController trafficCarController = car.GetComponent<TrafficCarController>();
        trafficCarController.numOfRoad = 0;

        if (follower == null)
        {
            follower = car.AddComponent<SplineFollower>();
        }

        follower.spline = citySpline;
        follower.follow = false;
        follower.followSpeed = speed;
        follower.wrapMode = SplineFollower.Wrap.Loop;

        Vector3 position = citySpline.EvaluatePosition(t);
        car.transform.position = position;

        follower.SetPercent(t);
        follower.Rebuild();

        StartCoroutine(EnableFollow(car, follower, t));
    }

    private IEnumerator EnableFollow(GameObject car, SplineFollower follower, double t)
    {
        yield return new WaitForSeconds(0.1f);

        follower.follow = true;
        Vector3 postActivationPosition = car.transform.position;

        if (citySpline != null && Vector3.Distance(postActivationPosition, citySpline.EvaluatePosition(t)) > 0.1f)
        {
            Debug.LogWarning($"������� ����� ��������� �� ���������! t = {t}, ������ = {citySpline.name}, ������� = {postActivationPosition}");
        }
    }
}