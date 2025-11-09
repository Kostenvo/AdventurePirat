using System;
using GameData;
using UnityEngine;

namespace GameObjects
{
    public class RestoreStateComponent :MonoBehaviour
    {
        [SerializeField] private string _id;
        private GameSession _gameSession;

        private void Start()
        {
            _gameSession = _gameSession ?? FindFirstObjectByType<GameSession>();
            if (_gameSession.IsStoredGo(_id)) Destroy(gameObject);
        }

        public void StoreInSession()
        {
            _gameSession = _gameSession ?? FindFirstObjectByType<GameSession>();
            _gameSession.StoreGo(_id);
        }
    }
}