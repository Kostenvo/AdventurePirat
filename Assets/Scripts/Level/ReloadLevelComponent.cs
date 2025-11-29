using GameData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class ReloadLevelComponent :MonoBehaviour
    {
        public void Reload()
        {
            
           
            GameSession.Instance.Load();
            var scene = SceneManager.GetActiveScene();
            var levelLoader = FindAnyObjectByType<LevelLoader>();
            levelLoader.LoadWithLoader(scene.name);
        }
    }
}