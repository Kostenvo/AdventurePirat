using System;
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

        protected override int _currentHealth
        {
            get => currentHealth = _gameSession.PlayerData.Health;
            set => currentHealth = _gameSession.PlayerData.Health = value;
        }

        private void Start()
        {
            _gameSession = FindAnyObjectByType<GameSession>();
            _currentHealth = _gameSession.PlayerData.Health;
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
    }
}