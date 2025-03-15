using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class MeshCombiner : EditorWindow
{
    [MenuItem("Tools/Combine Selected Meshes in Prefab (Ignore Materials)")]
    public static void CombineSelectedMeshesInPrefab()
    {
        // Проверяем, открыт ли префаб в редакторе (Prefab Mode)
        var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        if (prefabStage == null)
        {
            Debug.LogWarning("No prefab is currently open in the editor. Please open a prefab in Prefab Mode.");
            return;
        }

        // Получаем корневой объект открытого префаба
        GameObject prefabRoot = prefabStage.prefabContentsRoot;
        if (prefabRoot == null)
        {
            Debug.LogWarning("Failed to get prefab root from the open Prefab Stage.");
            return;
        }

        // Получаем выделенные объекты
        GameObject[] selectedObjects = Selection.gameObjects;
        if (selectedObjects.Length == 0)
        {
            Debug.LogWarning("No objects selected. Please select one or more objects inside the prefab.");
            return;
        }

        // Проверяем, что выделенные объекты находятся внутри текущего префаба
        foreach (var selectedObject in selectedObjects)
        {
            if (!selectedObject.transform.IsChildOf(prefabRoot.transform))
            {
                Debug.LogWarning($"Selected object '{selectedObject.name}' is not part of the open prefab '{prefabRoot.name}'.");
                return;
            }
        }

        // Находим все MeshFilter среди выделенных объектов
        List<MeshFilter> meshFilters = new List<MeshFilter>();
        foreach (var selectedObject in selectedObjects)
        {
            MeshFilter[] filters = selectedObject.GetComponentsInChildren<MeshFilter>();
            meshFilters.AddRange(filters);
        }

        if (meshFilters.Count == 0)
        {
            Debug.LogWarning("No MeshFilters found in the selected objects.");
            return;
        }

        // Собираем все меши для объединения
        List<CombineInstance> combineInstances = new List<CombineInstance>();
        foreach (MeshFilter meshFilter in meshFilters)
        {
            if (meshFilter.sharedMesh != null)
            {
                CombineInstance ci = new CombineInstance
                {
                    mesh = meshFilter.sharedMesh,
                    transform = meshFilter.transform.localToWorldMatrix // Преобразуем в мировые координаты
                };
                combineInstances.Add(ci);

                // Отключаем исходный объект
                meshFilter.gameObject.SetActive(false);
            }
        }

        if (combineInstances.Count == 0)
        {
            Debug.LogWarning("No valid meshes to combine in the selected objects.");
            return;
        }

        // Создаём новый меш
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combineInstances.ToArray(), true, true); // true для объединения всех вершин в один меш

        // Логируем количество вершин и треугольников
        Debug.Log($"Combined mesh: {combinedMesh.vertexCount} vertices, {combinedMesh.triangles.Length / 3} triangles");

        // Создаём новый объект с объединённым мешем
        GameObject combinedObject = new GameObject("CombinedMesh");
        combinedObject.transform.SetParent(prefabRoot.transform, false); // Добавляем в корень префаба
        MeshFilter combinedFilter = combinedObject.AddComponent<MeshFilter>();
        combinedFilter.sharedMesh = combinedMesh;

        // Добавляем MeshRenderer без материалов (можно добавить позже, если нужно)
        combinedObject.AddComponent<MeshRenderer>();

        // Сохраняем объединённый меш как .asset
        string meshPath = AssetDatabase.GenerateUniqueAssetPath("Assets/CarPrefabs/SimplifiedMeshes/CombinedMesh.mesh");
        AssetDatabase.CreateAsset(combinedMesh, meshPath);
        AssetDatabase.SaveAssets();

        // Сохраняем изменения в префаб
        PrefabUtility.SaveAsPrefabAsset(prefabRoot, prefabStage.assetPath);

        Debug.Log($"Combined {combineInstances.Count} meshes into {meshPath}.");
    }
}