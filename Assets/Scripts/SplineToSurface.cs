using UnityEngine;
using Dreamteck.Splines;

public class SplineSnapToSurface : MonoBehaviour
{
    [SerializeField] private SplineComputer[] splines;
    [SerializeField] private float heightOffset = 0.1f;
    [SerializeField] private float raycastDistance = 100f;
    [SerializeField] private LayerMask surfaceLayer;

    private void Start()
    {
        SnapSplinesToSurface();
    }

    private void SnapSplinesToSurface()
    {
        foreach (SplineComputer spline in splines)
        {
            SplinePoint[] points = spline.GetPoints();

            for (int i = 0; i < points.Length; i++)
            {
                Vector3 pointPosition = points[i].position;
                Ray ray = new Ray(pointPosition + Vector3.up * raycastDistance, Vector3.down);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, raycastDistance * 2, surfaceLayer))
                {
                    pointPosition.y = hit.point.y + heightOffset;
                    points[i].position = pointPosition;
                }
            }

            spline.SetPoints(points);
            spline.Rebuild();
        }
    }
}