using System;
using Subscribe;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameData
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        public PlayerData PlayerData => _playerData;
        public QuickInventoryModel QuickInventory => _quickInventory;
        
        private ComposideDisposible _trash = new ComposideDisposible();

        private PlayerData _saveData;
        private QuickInventoryModel _quickInventory;


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
            InitUI();
        }

        private void InitUI()
        {
            _quickInventory = new QuickInventoryModel(_playerData);
            _trash.Retain(new ActionDisposable(_quickInventory.Dispose));
            SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
        }

        private bool IsSessionExist()
        {
            var sessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None);
            foreach (var session in sessions)
            {
                if (session != this)
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
            OnDestroy();
            _quickInventory = new QuickInventoryModel(_playerData);
            _trash.Retain(new ActionDisposable(_quickInventory.Dispose));
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}