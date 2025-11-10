using UnityEngine;

namespace CameraSet
{
    public class CanvasCameraAssigner: MonoBehaviour
    {
        private Canvas targetCanvas;

        void Awake()
        {
            targetCanvas = GetComponent<Canvas>();
            // Попытка найти камеру сразу. 
            // Если главная сцена уже загружена, это сработает.
            AssignCamera();
        }
    
        // Если сцена с UI загружается аддитивно после главной сцены,
        // Awake может сработать до того, как камера главной сцены будет доступна.
        // Используем корутину для задержки на один кадр.
        void Start()
        {
            if (targetCanvas.worldCamera == null)
            {
                StartCoroutine(AssignCameraAfterDelay());
            }
        }

        System.Collections.IEnumerator AssignCameraAfterDelay()
        {
            // Ждем один кадр, чтобы гарантировать, что все сцены загружены
            yield return null; 
            AssignCamera();
        }

        private void AssignCamera()
        {
            if (targetCanvas != null && targetCanvas.worldCamera == null)
            {
                // Используем Camera.main для поиска камеры с тегом "MainCamera"
                UnityEngine.Camera mainCam = UnityEngine.Camera.main;
                if (mainCam != null)
                {
                    targetCanvas.worldCamera = mainCam;
                    Debug.Log("Assigned Main Camera to Canvas: " + gameObject.name);
                }
                else
                {
                    Debug.LogError("Main Camera not found in any active scene!");
                }
            }
        }
    }
}