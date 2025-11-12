using System;
using Definitions;
using GameData;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Perks
{
    public class IconPerk : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _fillArea;
        private GameSession _gameSession;
        private PerkDef _def;

        public void SetPerk(PerkDef def, GameSession gameSession)
        {
            _gameSession = gameSession;
            if (string.IsNullOrEmpty(def.Name))
            {
                gameObject.SetActive(false);
                return;
            }
            _def = def;
            _icon.sprite = def.IconImage;
            gameSession.PerksModel.CooldownPerk.SetTimeCooldown(def.Cooldown);
            gameObject.SetActive(true);
        }

        private void Update()
        {
             _fillArea.fillAmount = _gameSession.PerksModel.CooldownPerk.RemainingTime() / _def.Cooldown;
        }
    }
}