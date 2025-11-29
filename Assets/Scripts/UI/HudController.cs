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
        private ComposideDisposible trash = new ComposideDisposible();
        private Canvas _canvas;

        private void Start()
        {
            trash.Retain(GameSession.Instance.PlayerData.Health.Subscribe(ChangeValue));
            trash.Retain(GameSession.Instance.StatsModel.Subscribe(UpdateStats));
            trash.Retain(GameSession.Instance.PerksModel.SubscribeToActivePerk(UpdatePerks));
            ChangeValue(GameSession.Instance.PlayerData.Health.Value, 0);
            var ActionPerk = GameSession.Instance.PerksModel.ActivePerk;
            UpdatePerks(ActionPerk, "");
        }

        private void UpdatePerks(string newvalue, string oldvalue)
        {
            var def = DefsFacade.Instance.Perks.GetItem(newvalue);
            _iconPerk.SetPerk(def, GameSession.Instance);
        }


        private void UpdateStats()
        {
            if (GameSession.Instance.StatsModel.SelectedStats.Value == StatsType.Health)
            {
                ChangeValue(GameSession.Instance.PlayerData.Health.Value, 0);
            }
        }

        private void ChangeValue(int newvalue, int oldvalue)
        {
            var maxHealth = GameSession.Instance.StatsModel.GetLevel(StatsType.Health).Value;
            var normalizedHealth = (float)newvalue / maxHealth;
            _fillImageHealth.fillAmount = normalizedHealth;
        }

        public void OptionMenuButton() => LoadMenu.Load("UI/GameMenu");
        public void PerksMenuButton() => LoadMenu.Load("UI/PerkManagerWindow");
        public void StatsMenuButton() => LoadMenu.Load("UI/StatsManagerWindow");

        private void OnDestroy() => trash.Dispose();
    }
}