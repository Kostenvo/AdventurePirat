using UnityEngine;

namespace GameData
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        public PlayerData PlayerData => _playerData;
        private PlayerData _saveData;


        private void Awake()
        {
            if (IsSessionExist())
            {
                DestroyImmediate(this.gameObject);
            }
            else
            {
                DontDestroyOnLoad(this.gameObject);
                Save();
            }
        }
        
        private bool IsSessionExist()
        {
            var sessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None);
            foreach (var session in sessions)          
            {
                if (session !=this)
                {
                    return true;
                }
            }
            return false;
        }

        public void Save()
        {
          _saveData = _playerData.CloneData();
        }

        public void Load()
        {
            _playerData = _saveData.CloneData();
        }
    }
}