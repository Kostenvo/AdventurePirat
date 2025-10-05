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
            currentSession.Load();
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}