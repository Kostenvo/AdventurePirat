using System;
using GameData;
using TimeComponent;
using UnityEngine;

namespace Creatures.Hero
{
    public class ShieldComponent: MonoBehaviour
    {
        private GameSession _gameSession;
        private Cooldown PerkCooldown => _gameSession.PerksModel.CooldownPerk;

        private void Start()
        {
            _gameSession = FindFirstObjectByType<GameSession>();
        }

        public void UseShield()
        {
            if (!_gameSession.PerksModel.IsActivePerk("Shield") || !PerkCooldown.IsReady()) return;
            PerkCooldown.ResetCooldown();
            gameObject.SetActive(true);
        }

        private void Update()
        {
             if(PerkCooldown.IsReady()) gameObject.SetActive(false);
        }
    }
}