using GameData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class ReloadLevelComponent :MonoBehaviour
    {
        public void Reload()
        {
            var currentSession = FindAnyObjectByType<GameSession>();
            Destroy(currentSession.gameObject);
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}