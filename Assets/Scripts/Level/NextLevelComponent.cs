using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class NextLevelComponent : MonoBehaviour
    {
        [SerializeField] private string _nextLevelName;
        public void LoadLevel()
        {
            SceneManager.LoadScene(_nextLevelName);
        }
    }
}