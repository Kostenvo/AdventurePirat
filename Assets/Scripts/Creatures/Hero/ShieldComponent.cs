using System;
using GameData;
using Scripts.Creatures;
using TimeComponent;
using UnityEngine;

namespace Creatures.Hero
{
    public class ShieldComponent: MonoBehaviour
    {

        private HealthComponentBase _healthComponent;
        private Cooldown PerkCooldown => GameSession.Instance.PerksModel.CooldownPerk;

        private void Start()
        {
            _healthComponent ??= GetComponentInParent<HealthComponentBase>();
        }

        public void UseShield()
        {
            if (!GameSession.Instance.PerksModel.IsActivePerk("Shield") || !PerkCooldown.IsReady()) return;
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