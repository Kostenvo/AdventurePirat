using System;
using Definitions;
using GameData;
using Subscribe;
using UI.Perks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private Image _fillImageHealth;
        [SerializeField] private IconPerk _iconPerk;
        private GameSession _gameSession;
        private ComposideDisposible trash = new ComposideDisposible();
        private Canvas _canvas;

        private void Start()
        {
            _gameSession = FindFirstObjectByType<GameSession>();
            trash.Retain(_gameSession.PlayerData.Health.Subscribe(ChangeValue));
            trash.Retain(_gameSession.StatsModel.Subscribe(UpdateStats));
            trash.Retain(_gameSession.PerksModel.SubscribeToActivePerk(UpdatePerks));
            ChangeValue(_gameSession.PlayerData.Health.Value, 0);
            var ActionPerk = _gameSession.PerksModel.ActivePerk;
            UpdatePerks(ActionPerk, "");
        }

        private void UpdatePerks(string newvalue, string oldvalue)
        {
            var def = DefsFacade.Instance.Perks.GetItem(newvalue);
            _iconPerk.SetPerk(def, _gameSession);
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
            _fillImageHealth.fillAmount = normalizedHealth;
        }

        public void OptionMenuButton() => LoadMenu.Load("UI/GameMenu");
        public void PerksMenuButton() => LoadMenu.Load("UI/PerkManagerWindow");
        public void StatsMenuButton() => LoadMenu.Load("UI/StatsManagerWindow");

        private void OnDestroy() => trash.Dispose();
    }
}