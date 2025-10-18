using System;
using Definitions;
using GameData;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        private GameSession _gameSession;
        private void Start()
        {
            _gameSession = FindObjectOfType<GameSession>();
            _gameSession.PlayerData.Health.ValueChanged += ChangeValue;
            ChangeValue(_gameSession.PlayerData.Health.Value, 0);
        }

        private void ChangeValue(int newvalue, int oldvalue)
        {
            var maxHealth = DefsFacade.Instance.Player.MaxHealth;
            var normalizedHealth = (float)newvalue / maxHealth;
            _fillImage.fillAmount = normalizedHealth;
        }

        private void OnDestroy()
        {
            _gameSession.PlayerData.Health.ValueChanged -= ChangeValue;
        }
    }
}