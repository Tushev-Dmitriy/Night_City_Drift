using UnityEngine;
using Dreamteck.Splines;

public class LaneGenerator : MonoBehaviour
{
    [SerializeField] private SplineComputer baseSpline; // �������� ������ (���� ������)
    [SerializeField] private Transform parentContainer; // ��������� ��� ����� ��������
    [SerializeField] private float laneWidth = 4f; // ������ ����� ������
    [SerializeField] private int totalLanes = 6; // ����� ���������� �����
    [SerializeField] private float dividerGap = 2f; // ������ �������������� ������
    [SerializeField] private Vector3 offsetAxis = new Vector3(0, 0, 1); // ��� �������� (�� ��������� Z)

    private void Start()
    {
        GenerateLanes();
    }

    private void GenerateLanes()
    {
        if (baseSpline == null)
        {
            Debug.LogError("baseSpline �� ��������!");
            return;
        }

        if (parentContainer == null)
        {
            Debug.LogError("parentContainer �� ��������!");
            return;
        }

        // ���������� �������� ��� ������ ������
        int lanesRight = (totalLanes + 1) / 2; // 3 ������ ������
        int lanesLeft = totalLanes / 2; // 3 ������ �����

        // ������ ������� ��� ������ �������
        for (int i = 0; i < lanesRight; i++)
        {
            float offset = i * (laneWidth + dividerGap);
            CreateLane(offset, 1, $"LaneRight_{i + 1}"); // ������������� ��������
        }

        // ������ ������� ��� ����� �������
        for (int i = 0; i < lanesLeft; i++)
        {
            float offset = -((i + 1) * (laneWidth + dividerGap));
            CreateLane(offset, -1, $"LaneLeft_{i + 1}"); // ������������� ��������
        }
    }

    private void CreateLane(float offset, int direction, string laneName)
    {
        // ��������� �������� ������
        GameObject newLaneObj = Instantiate(baseSpline.gameObject, parentContainer);
        newLaneObj.name = laneName;
        SplineComputer newSpline = newLaneObj.GetComponent<SplineComputer>();

        // �������� ����� ��������� �������
        SplinePoint[] points = newSpline.GetPoints();

        // ������� ������ �����
        for (int i = 0; i < points.Length; i++)
        {
            points[i].position += offsetAxis * offset;
        }

        // ������������� ����� ����� � ������
        newSpline.SetPoints(points);

        // ������������� ������
        newSpline.Rebuild();
        Debug.Log($"������ ������ {laneName} � ��������� {offset}");
    }
}