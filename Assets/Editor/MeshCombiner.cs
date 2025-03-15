using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class MeshCombiner : EditorWindow
{
    [MenuItem("Tools/Combine Selected Meshes in Prefab (Ignore Materials)")]
    public static void CombineSelectedMeshesInPrefab()
    {
        // ���������, ������ �� ������ � ��������� (Prefab Mode)
        var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        if (prefabStage == null)
        {
            Debug.LogWarning("No prefab is currently open in the editor. Please open a prefab in Prefab Mode.");
            return;
        }

        // �������� �������� ������ ��������� �������
        GameObject prefabRoot = prefabStage.prefabContentsRoot;
        if (prefabRoot == null)
        {
            Debug.LogWarning("Failed to get prefab root from the open Prefab Stage.");
            return;
        }

        // �������� ���������� �������
        GameObject[] selectedObjects = Selection.gameObjects;
        if (selectedObjects.Length == 0)
        {
            Debug.LogWarning("No objects selected. Please select one or more objects inside the prefab.");
            return;
        }

        // ���������, ��� ���������� ������� ��������� ������ �������� �������
        foreach (var selectedObject in selectedObjects)
        {
            if (!selectedObject.transform.IsChildOf(prefabRoot.transform))
            {
                Debug.LogWarning($"Selected object '{selectedObject.name}' is not part of the open prefab '{prefabRoot.name}'.");
                return;
            }
        }

        // ������� ��� MeshFilter ����� ���������� ��������
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

        // �������� ��� ���� ��� �����������
        List<CombineInstance> combineInstances = new List<CombineInstance>();
        foreach (MeshFilter meshFilter in meshFilters)
        {
            if (meshFilter.sharedMesh != null)
            {
                CombineInstance ci = new CombineInstance
                {
                    mesh = meshFilter.sharedMesh,
                    transform = meshFilter.transform.localToWorldMatrix // ����������� � ������� ����������
                };
                combineInstances.Add(ci);

                // ��������� �������� ������
                meshFilter.gameObject.SetActive(false);
            }
        }

        if (combineInstances.Count == 0)
        {
            Debug.LogWarning("No valid meshes to combine in the selected objects.");
            return;
        }

        // ������ ����� ���
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combineInstances.ToArray(), true, true); // true ��� ����������� ���� ������ � ���� ���

        // �������� ���������� ������ � �������������
        Debug.Log($"Combined mesh: {combinedMesh.vertexCount} vertices, {combinedMesh.triangles.Length / 3} triangles");

        // ������ ����� ������ � ����������� �����
        GameObject combinedObject = new GameObject("CombinedMesh");
        combinedObject.transform.SetParent(prefabRoot.transform, false); // ��������� � ������ �������
        MeshFilter combinedFilter = combinedObject.AddComponent<MeshFilter>();
        combinedFilter.sharedMesh = combinedMesh;

        // ��������� MeshRenderer ��� ���������� (����� �������� �����, ���� �����)
        combinedObject.AddComponent<MeshRenderer>();

        // ��������� ����������� ��� ��� .asset
        string meshPath = AssetDatabase.GenerateUniqueAssetPath("Assets/CarPrefabs/SimplifiedMeshes/CombinedMesh.mesh");
        AssetDatabase.CreateAsset(combinedMesh, meshPath);
        AssetDatabase.SaveAssets();

        // ��������� ��������� � ������
        PrefabUtility.SaveAsPrefabAsset(prefabRoot, prefabStage.assetPath);

        Debug.Log($"Combined {combineInstances.Count} meshes into {meshPath}.");
    }
}