using UnityEngine;
using UnityEditor;

public class LODGroupMenu
{
    [MenuItem("Tools/Auto Setup LOD Group on Selected Objects")]
    private static void AutoSetupLODGroup()
    {
        // �������� ��� ���������� �������
        foreach (GameObject selectedObject in Selection.gameObjects)
        {
            // ���������, ���� �� � ������� LOD Group
            LODGroup lodGroup = selectedObject.GetComponent<LODGroup>();
            if (lodGroup == null)
            {
                lodGroup = selectedObject.AddComponent<LODGroup>(); // ���������, ���� ���
            }

            // �������� ��� ������� � �������� ��������
            Renderer[] renderers = selectedObject.GetComponentsInChildren<Renderer>();

            if (renderers.Length == 0)
            {
                Debug.LogWarning($"No renderers found in {selectedObject.name}. Skipping...");
                continue;
            }

            // ����������� LOD ������
            LOD lod0 = new LOD(0.7f, renderers); // LOD 0: ������ �����������
            LOD lod1 = new LOD(0.4f, new Renderer[0]); // LOD 1: ������ (����� ��������� �����)
            LOD lod2 = new LOD(0.1f, new Renderer[0]); // LOD 2: ������
            LOD cull = new LOD(0.01f, new Renderer[0]); // Cull: ����������

            lodGroup.SetLODs(new LOD[] { lod0, lod1, lod2, cull });
            lodGroup.RecalculateBounds();

            Debug.Log($"Auto setup LOD Group for {selectedObject.name} with {renderers.Length} renderers!");
        }
    }

    [MenuItem("Tools/Auto Setup LOD Group on Selected Objects", true)]
    private static bool ValidateAutoSetupLODGroup()
    {
        // �������� ���� ������ ���� ���-�� ��������
        return Selection.activeGameObject != null;
    }
}