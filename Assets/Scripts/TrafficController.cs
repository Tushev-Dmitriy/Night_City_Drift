using UnityEngine;
using Dreamteck.Splines;
using System.Collections;

public class TrafficController : MonoBehaviour
{
    [SerializeField] private SplineComputer spline;
    [SerializeField] private Transform agentsContainer;
    [SerializeField] private GameObject[] carPrefabs;
    [SerializeField] private float speed = 10f;
    [SerializeField] private int initialCarCount = 50;

    private void Start()
    {
        spline.sampleMode = SplineComputer.SampleMode.Uniform;

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
        if (follower == null)
        {
            follower = car.AddComponent<SplineFollower>();
        }

        follower.spline = spline;
        follower.follow = false;
        follower.followSpeed = speed;
        follower.wrapMode = SplineFollower.Wrap.Loop;

        Vector3 position = spline.EvaluatePosition(t);
        car.transform.localPosition = position;

        follower.SetPercent(t);
        follower.Rebuild();

        StartCoroutine(EnableFollow(car, follower, t));
    }

    private IEnumerator EnableFollow(GameObject car, SplineFollower follower, double t)
    {
        yield return new WaitForSeconds(0.1f);

        follower.follow = true;

        Vector3 postActivationPosition = car.transform.localPosition;
        if (Vector3.Distance(postActivationPosition, spline.EvaluatePosition(t)) > 0.1f)
        {
            Debug.LogWarning($"ѕозици€ после активации не совпадает! t = {t}, позици€ = {postActivationPosition}");
        }
    }
}