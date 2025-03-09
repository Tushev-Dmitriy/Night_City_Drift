using UnityEngine;
using UnityEditor;

public class LODGroupMenu
{
    [MenuItem("Tools/Auto Setup LOD Group on Selected Objects")]
    private static void AutoSetupLODGroup()
    {
        // Получаем все выделенные объекты
        foreach (GameObject selectedObject in Selection.gameObjects)
        {
            // Проверяем, есть ли у объекта LOD Group
            LODGroup lodGroup = selectedObject.GetComponent<LODGroup>();
            if (lodGroup == null)
            {
                lodGroup = selectedObject.AddComponent<LODGroup>(); // Добавляем, если нет
            }

            // Получаем все рендеры в дочерних объектах
            Renderer[] renderers = selectedObject.GetComponentsInChildren<Renderer>();

            if (renderers.Length == 0)
            {
                Debug.LogWarning($"No renderers found in {selectedObject.name}. Skipping...");
                continue;
            }

            // Настраиваем LOD уровни
            LOD lod0 = new LOD(0.7f, renderers); // LOD 0: полная детализация
            LOD lod1 = new LOD(0.4f, new Renderer[0]); // LOD 1: пустой (можно настроить позже)
            LOD lod2 = new LOD(0.1f, new Renderer[0]); // LOD 2: пустой
            LOD cull = new LOD(0.01f, new Renderer[0]); // Cull: отключение

            lodGroup.SetLODs(new LOD[] { lod0, lod1, lod2, cull });
            lodGroup.RecalculateBounds();

            Debug.Log($"Auto setup LOD Group for {selectedObject.name} with {renderers.Length} renderers!");
        }
    }

    [MenuItem("Tools/Auto Setup LOD Group on Selected Objects", true)]
    private static bool ValidateAutoSetupLODGroup()
    {
        // Включаем меню только если что-то выделено
        return Selection.activeGameObject != null;
    }
}