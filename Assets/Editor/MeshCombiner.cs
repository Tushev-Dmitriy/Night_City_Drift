using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class MeshCombiner : EditorWindow
{
    [MenuItem("Tools/Combine Meshes with Materials in Open Prefab")]
    public static void CombineMeshesWithMaterialsInOpenPrefab()
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

        // Находим все MeshFilter и их MeshRenderer
        MeshFilter[] meshFilters = prefabRoot.GetComponentsInChildren<MeshFilter>();
        List<CombineInstance> combineInstances = new List<CombineInstance>();
        List<Material> materials = new List<Material>();
        List<int> subMeshIndices = new List<int>();

        int totalSubMeshCount = 0;

        foreach (MeshFilter meshFilter in meshFilters)
        {
            if (meshFilter.sharedMesh == null || meshFilter.GetComponent<MeshRenderer>() == null) continue;

            MeshRenderer renderer = meshFilter.GetComponent<MeshRenderer>();
            if (renderer.sharedMaterials == null || renderer.sharedMaterials.Length == 0) continue;

            // Добавляем каждый материал и соответствующий subMesh
            Material[] objectMaterials = renderer.sharedMaterials;
            for (int i = 0; i < objectMaterials.Length; i++)
            {
                if (objectMaterials[i] != null)
                {
                    int materialIndex = materials.IndexOf(objectMaterials[i]);
                    if (materialIndex < 0)
                    {
                        materials.Add(objectMaterials[i]);
                        materialIndex = materials.Count - 1;
                    }

                    CombineInstance ci = new CombineInstance
                    {
                        mesh = meshFilter.sharedMesh,
                        subMeshIndex = i, // Индекс subMesh для текущего материала
                        transform = meshFilter.transform.localToWorldMatrix
                    };
                    combineInstances.Add(ci);
                    subMeshIndices.Add(materialIndex); // Сохраняем индекс материала для этого subMesh
                    totalSubMeshCount++;
                }
            }

            // Отключаем объект, чтобы он не мешал
            meshFilter.gameObject.SetActive(false);
        }

        // Создаём новый меш
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combineInstances.ToArray(), false, true); // false для сохранения subMesh'ей

        // Логируем количество вершин и треугольников
        Debug.Log($"Combined mesh: {combinedMesh.vertexCount} vertices, {combinedMesh.triangles.Length / 3} triangles across {totalSubMeshCount} sub-meshes");

        // Создаём новый объект с объединённым мешем
        GameObject combinedObject = new GameObject("CombinedMesh");
        combinedObject.transform.SetParent(prefabRoot.transform, false);
        MeshFilter combinedFilter = combinedObject.AddComponent<MeshFilter>();
        combinedFilter.sharedMesh = combinedMesh;

        // Настраиваем MeshRenderer с материалами
        MeshRenderer combinedRenderer = combinedObject.AddComponent<MeshRenderer>();
        combinedRenderer.sharedMaterials = materials.ToArray();

        // Сохраняем объединённый меш как .asset
        string meshPath = AssetDatabase.GenerateUniqueAssetPath("Assets/CarPrefabs/Traffic/SimplifiedMeshes/CombinedMesh.mesh");
        AssetDatabase.CreateAsset(combinedMesh, meshPath);
        AssetDatabase.SaveAssets();

        // Сохраняем изменения в префаб
        PrefabUtility.SaveAsPrefabAsset(prefabRoot, prefabStage.assetPath);

        Debug.Log($"Combined {combineInstances.Count} meshes into {meshPath} with {materials.Count} materials.");
    }
}