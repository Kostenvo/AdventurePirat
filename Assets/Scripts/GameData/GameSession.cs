using UnityEngine;

namespace GameData
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        public PlayerData PlayerData => _playerData;


        private void Awake()
        {
            if (IsSessionExist())
            {
                Destroy(this.gameObject);
            }
            else
            {
                DontDestroyOnLoad(this.gameObject);
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
    }
}