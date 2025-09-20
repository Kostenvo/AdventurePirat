using UnityEngine;

public class ApplyRedMaterial : MonoBehaviour
{
    private void Awake()
    {
        // Создадим красный материал с URP шейдером
        var redMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        redMaterial.color = Color.red;

        // Найдем все сферы в сцене
        var allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        var appliedCount = 0;

        // Применим красный материал ко всем сферам
        foreach (var obj in allObjects)
            if (obj.name.StartsWith("Sphere"))
            {
                var renderer = obj.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.material = redMaterial;
                    appliedCount++;
                    Debug.Log($"Applied red material to {obj.name}");
                }
            }

        Debug.Log($"Red material applied to {appliedCount} spheres!");

        // Удалим этот компонент после применения материала
        Destroy(this);
    }
}