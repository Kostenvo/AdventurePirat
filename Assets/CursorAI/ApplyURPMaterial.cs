using UnityEngine;

public class ApplyURPMaterial : MonoBehaviour
{
    private void Start()
    {
        // Создадим материал с URP шейдером
        var urpMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));

        // Найдем все сферы в сцене
        var allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        var appliedCount = 0;

        // Применим материал ко всем сферам
        foreach (var obj in allObjects)
            if (obj.name.StartsWith("Sphere"))
            {
                var renderer = obj.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.material = urpMaterial;
                    appliedCount++;
                    Debug.Log($"Applied URP material to {obj.name}");
                }
            }

        Debug.Log($"URP material applied to {appliedCount} spheres!");

        // Удалим этот компонент после применения материала
        Destroy(this);
    }
}