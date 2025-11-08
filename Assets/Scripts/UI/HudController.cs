using System;
using Definitions;
using GameData;
using Subscribe;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        private GameSession _gameSession;
        private ComposideDisposible trash = new ComposideDisposible();
        private Canvas _canvas;
        private void Start()
        {
            _gameSession = FindObjectOfType<GameSession>();
            trash.Retain(_gameSession.PlayerData.Health.Subscribe(ChangeValue));
            trash.Retain(_gameSession.StatsModel.Subscribe(UpdateStats));
            ChangeValue(_gameSession.PlayerData.Health.Value, 0);
            
        }

        private void UpdateStats()
        {
            if (_gameSession.StatsModel.SelectedStats.Value == StatsType.Health)
            {
                ChangeValue(_gameSession.PlayerData.Health.Value, 0);
            }
        }

        private void ChangeValue(int newvalue, int oldvalue)
        {
            var maxHealth = _gameSession.StatsModel.GetLevel(StatsType.Health).Value;
            var normalizedHealth = (float)newvalue / maxHealth;
            _fillImage.fillAmount = normalizedHealth;
        }
        public void OptionMenuButton() => LoadMenu.Load("UI/GameMenu");
        public void PerksMenuButton() => LoadMenu.Load("UI/PerkManagerWindow");
        public void StatsMenuButton() => LoadMenu.Load("UI/StatsManagerWindow");

        private void OnDestroy() => trash.Dispose();
    }
}