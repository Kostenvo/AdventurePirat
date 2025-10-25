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
            ChangeValue(_gameSession.PlayerData.Health.Value, 0);
        }

        private void ChangeValue(int newvalue, int oldvalue)
        {
            var maxHealth = DefsFacade.Instance.Player.MaxHealth;
            var normalizedHealth = (float)newvalue / maxHealth;
            _fillImage.fillAmount = normalizedHealth;
        }
        public void OptionMenuButton()
        {
            LoadMenu.Load("UI/GameMenu");
        }

        private void OnDestroy()
        {
            trash.Dispose();
        }
    }
}