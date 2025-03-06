using UnityEngine;
using Dreamteck.Splines;

public class LaneGenerator : MonoBehaviour
{
    [SerializeField] private SplineComputer baseSpline; // Исходный сплайн (одна полоса)
    [SerializeField] private Transform parentContainer; // Контейнер для новых сплайнов
    [SerializeField] private float laneWidth = 4f; // Ширина одной полосы
    [SerializeField] private int totalLanes = 6; // Общее количество полос
    [SerializeField] private float dividerGap = 2f; // Ширина разделительной полосы
    [SerializeField] private Vector3 offsetAxis = new Vector3(0, 0, 1); // Ось смещения (по умолчанию Z)

    private void Start()
    {
        GenerateLanes();
    }

    private void GenerateLanes()
    {
        if (baseSpline == null)
        {
            Debug.LogError("baseSpline не назначен!");
            return;
        }

        if (parentContainer == null)
        {
            Debug.LogError("parentContainer не назначен!");
            return;
        }

        // Определяем смещение для каждой полосы
        int lanesRight = (totalLanes + 1) / 2; // 3 полосы вправо
        int lanesLeft = totalLanes / 2; // 3 полосы влево

        // Создаём сплайны для правой стороны
        for (int i = 0; i < lanesRight; i++)
        {
            float offset = i * (laneWidth + dividerGap);
            CreateLane(offset, 1, $"LaneRight_{i + 1}"); // Положительное смещение
        }

        // Создаём сплайны для левой стороны
        for (int i = 0; i < lanesLeft; i++)
        {
            float offset = -((i + 1) * (laneWidth + dividerGap));
            CreateLane(offset, -1, $"LaneLeft_{i + 1}"); // Отрицательное смещение
        }
    }

    private void CreateLane(float offset, int direction, string laneName)
    {
        // Дублируем исходный сплайн
        GameObject newLaneObj = Instantiate(baseSpline.gameObject, parentContainer);
        newLaneObj.name = laneName;
        SplineComputer newSpline = newLaneObj.GetComponent<SplineComputer>();

        // Получаем точки исходного сплайна
        SplinePoint[] points = newSpline.GetPoints();

        // Смещаем каждую точку
        for (int i = 0; i < points.Length; i++)
        {
            points[i].position += offsetAxis * offset;
        }

        // Устанавливаем новые точки в сплайн
        newSpline.SetPoints(points);

        // Перестраиваем сплайн
        newSpline.Rebuild();
        Debug.Log($"Создан сплайн {laneName} с смещением {offset}");
    }
}