using System;
using Definitions;
using GameData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Creatures.Hero
{
    public class HeroHealthComponent : HealthComponentBase
    {
        [SerializeField] private Animator _animator;
        private GameSession _gameSession;
        private static readonly int Health = Animator.StringToHash("Health");
        private static string _poisonKey = "Poison";
        public override int MaxHealth => DefsFacade.Instance.Player.MaxHealth;

        protected override int _currentHealth
        {
            get =>  _gameSession.PlayerData.Health.Value;
            set =>  _gameSession.PlayerData.Health.Value = value;
        }

        private void Start()
        {
            _gameSession = FindAnyObjectByType<GameSession>();
            _currentHealth = _gameSession.PlayerData.Health.Value;
        }

        protected override void Damage(int amount)
        {
            base.Damage(amount);
            _animator.SetInteger(Health, _currentHealth);
        }

        protected override void Heal(int amount)
        {
            base.Heal(amount);
            _animator.SetInteger(Health, _currentHealth);
        }

        public void HeroHeal()
        {
            if (_gameSession.PlayerData.Inventory.CountItem(_poisonKey) < 1) return;
            _gameSession.PlayerData.Inventory.RemoveItem(_poisonKey, 1);
            Heal(10);
        }
    }
}