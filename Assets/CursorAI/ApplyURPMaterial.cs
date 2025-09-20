using UnityEngine;

public class ApplyURPMaterial : MonoBehaviour
{
    void Start()
    {
        // Создадим материал с URP шейдером
        Material urpMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        
        // Найдем все сферы в сцене
        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        
        int appliedCount = 0;
        
        // Применим материал ко всем сферам
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.StartsWith("Sphere"))
            {
                MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.material = urpMaterial;
                    appliedCount++;
                    Debug.Log($"Applied URP material to {obj.name}");
                }
            }
        }
        
        Debug.Log($"URP material applied to {appliedCount} spheres!");
        
        // Удалим этот компонент после применения материала
        Destroy(this);
    }
}


