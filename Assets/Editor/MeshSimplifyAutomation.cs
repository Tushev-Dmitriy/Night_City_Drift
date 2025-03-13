using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;

public class MeshSimplifyAutomation : EditorWindow
{
    [MenuItem("Tools/Simplify All Meshes in Open Prefab")]
    public static void SimplifyAllMeshesInOpenPrefab()
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

        int simplifiedCount = 0;
        string prefabPath = prefabStage.assetPath;

        // ������ ����� ��� ���������� ���������� �����
        string meshSaveFolder = Path.Combine(Path.GetDirectoryName(prefabPath), "SimplifiedMeshes");
        if (!Directory.Exists(meshSaveFolder))
        {
            Directory.CreateDirectory(meshSaveFolder);
            AssetDatabase.Refresh();
        }

        // �������� ��� ������ MeshSimplify �� �������� �������
        MeshSimplify rootMeshSimplify = prefabRoot.GetComponent<MeshSimplify>();
        if (rootMeshSimplify == null)
        {
            rootMeshSimplify = prefabRoot.AddComponent<MeshSimplify>();
        }

        // �������� ComputeData ��� ���������� ������
        Debug.Log("Computing data for mesh simplification...");
        rootMeshSimplify.ComputeData(true);

        // ������� ��� ���� � ������� (������� ��������)
        MeshFilter[] meshFilters = prefabRoot.GetComponentsInChildren<MeshFilter>();
        SkinnedMeshRenderer[] skinnedRenderers = prefabRoot.GetComponentsInChildren<SkinnedMeshRenderer>();

        // ������������ ����� ���������� ������ �� ���������
        int totalVerticesBefore = 0;
        foreach (MeshFilter mf in meshFilters)
        {
            if (mf.sharedMesh != null) totalVerticesBefore += mf.sharedMesh.vertexCount;
        }
        foreach (SkinnedMeshRenderer smr in skinnedRenderers)
        {
            if (smr.sharedMesh != null) totalVerticesBefore += smr.sharedMesh.vertexCount;
        }
        Debug.Log($"Total vertices before simplification: {totalVerticesBefore}");

        // ������������ ��� MeshFilter
        foreach (MeshFilter meshFilter in meshFilters)
        {
            GameObject obj = meshFilter.gameObject;
            MeshSimplify meshSimplify = obj.GetComponent<MeshSimplify>();
            if (meshSimplify == null)
            {
                meshSimplify = obj.AddComponent<MeshSimplify>();
                Debug.Log($"Added MeshSimplify to {obj.name} in prefab {prefabRoot.name}");
            }

            // ����������� ���������
            meshSimplify.m_bGenerateIncludeChildren = true;
            meshSimplify.m_bEnablePrefabUsage = true;
            meshSimplify.m_fVertexAmount = 0.5f; // ��������� �� 50%
            meshSimplify.ConfigureSimplifier();

            // ��������� ����������� �� ���������
            meshSimplify.m_meshSimplifier.UseEdgeLength = true;
            meshSimplify.m_meshSimplifier.UseCurvature = false;
            meshSimplify.m_meshSimplifier.ProtectTexture = false;
            meshSimplify.m_meshSimplifier.LockBorder = false;

            // �������� �������� ���������� ������ � �������������
            Mesh originalMesh = meshFilter.sharedMesh;
            if (originalMesh != null)
            {
                Debug.Log($"Original mesh for {obj.name}: {originalMesh.vertexCount} vertices, {originalMesh.triangles.Length / 3} triangles");
            }
            else
            {
                Debug.LogWarning($"Original mesh for {obj.name} is null. Skipping...");
                continue;
            }

            // ��������� ���������� ���
            Debug.Log($"Computing simplified mesh for {obj.name}...");
            int targetVertexCount = Mathf.Max(4, Mathf.RoundToInt(originalMesh.vertexCount * 0.5f)); // ������� 4 �������
            meshSimplify.m_meshSimplifier.ComputeMeshWithVertexCount(obj, meshSimplify.m_simplifiedMesh, targetVertexCount, $"{obj.name} Simplified");

            // ���������, ������ �� ���������� ���
            if (meshSimplify.m_simplifiedMesh != null && meshSimplify.m_simplifiedMesh.vertexCount > 0)
            {
                // �������� ���������� ������ � ������������� ����� ���������
                Debug.Log($"Simplified mesh for {obj.name}: {meshSimplify.m_simplifiedMesh.vertexCount} vertices, {meshSimplify.m_simplifiedMesh.triangles.Length / 3} triangles");

                // ��������� ���������� ��� ��� .asset
                string meshName = $"{obj.name}_simplified.mesh";
                string meshPath = Path.Combine(meshSaveFolder, meshName);
                meshPath = AssetDatabase.GenerateUniqueAssetPath(meshPath);
                AssetDatabase.CreateAsset(meshSimplify.m_simplifiedMesh, meshPath);
                AssetDatabase.SaveAssets();

                // ��������� ������ �� ����� .asset
                meshFilter.sharedMesh = meshSimplify.m_simplifiedMesh;
                simplifiedCount++;

                Debug.Log($"Saved simplified mesh for {obj.name} to {meshPath} and applied to prefab.");
            }
            else
            {
                Debug.LogWarning($"No simplified mesh generated for {obj.name}. Skipping...");
            }
        }

        // ������������ ��� SkinnedMeshRenderer
        foreach (SkinnedMeshRenderer skinnedRenderer in skinnedRenderers)
        {
            GameObject obj = skinnedRenderer.gameObject;
            MeshSimplify meshSimplify = obj.GetComponent<MeshSimplify>();
            if (meshSimplify == null)
            {
                meshSimplify = obj.AddComponent<MeshSimplify>();
                Debug.Log($"Added MeshSimplify to {obj.name} in prefab {prefabRoot.name}");
            }

            // ����������� ���������
            meshSimplify.m_bGenerateIncludeChildren = true;
            meshSimplify.m_bEnablePrefabUsage = true;
            meshSimplify.m_fVertexAmount = 0.5f;
            meshSimplify.ConfigureSimplifier();

            // ��������� ����������� �� ���������
            meshSimplify.m_meshSimplifier.UseEdgeLength = true;
            meshSimplify.m_meshSimplifier.UseCurvature = false;
            meshSimplify.m_meshSimplifier.ProtectTexture = false;
            meshSimplify.m_meshSimplifier.LockBorder = false;

            // �������� �������� ���������� ������ � �������������
            Mesh originalMesh = skinnedRenderer.sharedMesh;
            if (originalMesh != null)
            {
                Debug.Log($"Original mesh for {obj.name}: {originalMesh.vertexCount} vertices, {originalMesh.triangles.Length / 3} triangles");
            }
            else
            {
                Debug.LogWarning($"Original mesh for {obj.name} is null. Skipping...");
                continue;
            }

            // ��������� ���������� ���
            Debug.Log($"Computing simplified mesh for {obj.name}...");
            int targetVertexCount = Mathf.Max(4, Mathf.RoundToInt(originalMesh.vertexCount * 0.5f)); // ������� 4 �������
            meshSimplify.m_meshSimplifier.ComputeMeshWithVertexCount(obj, meshSimplify.m_simplifiedMesh, targetVertexCount, $"{obj.name} Simplified");

            // ���������, ������ �� ���������� ���
            if (meshSimplify.m_simplifiedMesh != null && meshSimplify.m_simplifiedMesh.vertexCount > 0)
            {
                // �������� ���������� ������ � ������������� ����� ���������
                Debug.Log($"Simplified mesh for {obj.name}: {meshSimplify.m_simplifiedMesh.vertexCount} vertices, {meshSimplify.m_simplifiedMesh.triangles.Length / 3} triangles");

                // ��������� ���������� ��� ��� .asset
                string meshName = $"{obj.name}_simplified.mesh";
                string meshPath = Path.Combine(meshSaveFolder, meshName);
                meshPath = AssetDatabase.GenerateUniqueAssetPath(meshPath);
                AssetDatabase.CreateAsset(meshSimplify.m_simplifiedMesh, meshPath);
                AssetDatabase.SaveAssets();

                // ��������� ������ �� ����� .asset
                skinnedRenderer.sharedMesh = meshSimplify.m_simplifiedMesh;
                simplifiedCount++;

                Debug.Log($"Saved simplified mesh for {obj.name} to {meshPath} and applied to prefab.");
            }
            else
            {
                Debug.LogWarning($"No simplified mesh generated for {obj.name}. Skipping...");
            }
        }

        // ������������ ����� ���������� ������ ����� ���������
        int totalVerticesAfter = 0;
        foreach (MeshFilter mf in meshFilters)
        {
            if (mf.sharedMesh != null) totalVerticesAfter += mf.sharedMesh.vertexCount;
        }
        foreach (SkinnedMeshRenderer smr in skinnedRenderers)
        {
            if (smr.sharedMesh != null) totalVerticesAfter += smr.sharedMesh.vertexCount;
        }
        Debug.Log($"Total vertices after simplification: {totalVerticesAfter}");

        // ��������� ��������� � ������
        PrefabUtility.SaveAsPrefabAsset(prefabRoot, prefabPath);

        Debug.Log($"Saved changes to prefab: {prefabRoot.name} at path: {prefabPath}");
        Debug.Log($"Simplified {simplifiedCount} meshes in the open prefab.");
    }
}