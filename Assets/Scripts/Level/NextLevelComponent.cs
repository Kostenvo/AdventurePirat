using GameData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class NextLevelComponent : MonoBehaviour
    {
        [SerializeField] private string _nextLevelName;
        public void LoadLevel()
        {
            var currentSession = FindAnyObjectByType<GameSession>();
            var levelLoader = FindAnyObjectByType<LevelLoader>();
            currentSession.Save();
            levelLoader.LoadWithLoader(_nextLevelName);
        }
    }
}