using System;
using System.Collections.Generic;
using System.Linq;
using Interact.CheckPoint;
using Subscribe;
using UI;
using UI.QuickInventory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameData
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private string _defaultCheckPoint;
        public PlayerData PlayerData => _playerData;
        public PerkModel PerksModel => _perksModel;
        public QuickInventoryModel QuickInventory => _quickInventory;

        private ComposideDisposible _trash = new ComposideDisposible();

        private PlayerData _saveData;
        private PerkModel _perksModel;


        private QuickInventoryModel _quickInventory;

        private List<string> _checkPoints = new List<string>();

        private void Awake()
        {
            var findSession = GetSession();
            if (findSession)
            {
                findSession.AddCheckPoint(_defaultCheckPoint);
                findSession.InitUI();
                DestroyImmediate(this.gameObject);
            }
            else
            {
                AddCheckPoint(_defaultCheckPoint);
                InitUI();
                DontDestroyOnLoad(this.gameObject);
            }
        }

        private void InitUI()
        {
            Subscribe();
            SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
            SpawnHero();
        }

        private CheckPointComponent GetCheckPoint(string checkPointName)
        {
            var checkPointsOnLevel = FindObjectsByType<CheckPointComponent>(FindObjectsSortMode.None);
            foreach (var chPo in checkPointsOnLevel)
            {
                if (chPo.CheckPointName.Contains(checkPointName))
                {
                    return chPo;
                }
            }
            return null;
        }

        public bool IsCheckpointChecked(string checkPointName) => _checkPoints.Contains(checkPointName);

        private void SpawnHero()
        {
            var checkPoint = _checkPoints.Last();
            var lastCheckPoint = GetCheckPoint(checkPoint);
            lastCheckPoint.SpawnHero();
        }

        private GameSession GetSession()
        {
            var sessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None);
            foreach (var session in sessions)
            {
                if (session != this)
                {
                    return session;
                }
            }
            return null;
        }

        public void Save()
        {
            _saveData = _playerData.CloneData();
        }

        public void Load()
        {
            _playerData = _saveData.CloneData();
            OnDestroy();
            Subscribe();
        }

        private void Subscribe()
        {
            _quickInventory = new QuickInventoryModel(_playerData);
            _trash.Retain(new ActionDisposable(_quickInventory.Dispose));
            _perksModel = new PerkModel(_playerData);
        }

        public void AddCheckPoint(string checkPoint)
        {
            if (!_checkPoints.Contains(checkPoint))
            {
                _checkPoints.Add(checkPoint);
                Save();
            }
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}