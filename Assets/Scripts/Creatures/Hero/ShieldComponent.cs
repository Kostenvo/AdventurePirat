using System;
using GameData;
using Scripts.Creatures;
using TimeComponent;
using UnityEngine;

namespace Creatures.Hero
{
    public class ShieldComponent: MonoBehaviour
    {
        private GameSession _gameSession;
        private HealthComponentBase _healthComponent;
        private Cooldown PerkCooldown => _gameSession.PerksModel.CooldownPerk;

        private void Start()
        {
            _gameSession = FindFirstObjectByType<GameSession>();
            _healthComponent ??= GetComponentInParent<HealthComponentBase>();
        }

        public void UseShield()
        {
            if (!_gameSession.PerksModel.IsActivePerk("Shield") || !PerkCooldown.IsReady()) return;
            _healthComponent.Lock.AddLock(this);
            PerkCooldown.ResetCooldown();
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (PerkCooldown.IsReady())
            {
                _healthComponent.Lock.RemoveLock(this);
                gameObject.SetActive(false);
            }
        }
    }
}