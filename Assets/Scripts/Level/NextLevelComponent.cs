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
            var levelLoader = FindAnyObjectByType<LevelLoader>();
            GameSession.Instance.Save();
            levelLoader.LoadWithLoader(_nextLevelName);
        }
    }
}