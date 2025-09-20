using UnityEngine;

public class RedMaterialApplier : MonoBehaviour
{
    void Start()
    {
        ApplyRedMaterialToSpheres();
    }
    
    public void ApplyRedMaterialToSpheres()
    {
        // Создадим красный материал с URP шейдером
        Material redMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        redMaterial.color = Color.red;
        
        // Найдем все сферы в сцене
        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        
        int appliedCount = 0;
        
        // Применим красный материал ко всем сферам
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.StartsWith("Sphere"))
            {
                MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.material = redMaterial;
                    appliedCount++;
                    Debug.Log($"Applied red material to {obj.name}");
                }
            }
        }
        
        Debug.Log($"Red material applied to {appliedCount} spheres!");
        
        // Удалим этот компонент после применения материала
        Destroy(this);
    }
}


