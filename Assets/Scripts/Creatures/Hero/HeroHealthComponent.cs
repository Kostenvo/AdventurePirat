using Definitions;
using GameData;
using Scripts.Creatures;
using UnityEngine;

namespace Creatures.Hero
{
    public class HeroHealthComponent : HealthComponentBase
    {
        [SerializeField] private Animator _animator;
        private GameSession _gameSession;
        private static readonly int Health = Animator.StringToHash("Health");
        private InventoryItemDef _poisonKey => _gameSession.QuickInventory.GetCurrentItemDef();
        public override int MaxHealth => (int)_gameSession.StatsModel.GetLevel(StatsType.Health).Value;

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
            if (_gameSession.PlayerData.Inventory.CountItem(_poisonKey.Name) < 1) return;
            var healAmount = (int)DefsFacade.Instance.Potion.GetItem(_poisonKey.Name).EffectValue;
            _gameSession.PlayerData.Inventory.RemoveItem(_poisonKey.Name, 1);
            Heal(healAmount);
        }
        
    }
}