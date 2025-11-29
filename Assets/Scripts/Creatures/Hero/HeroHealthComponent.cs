using CameraSet;
using Definitions;
using GameData;
using Scripts.Creatures;
using TimeComponent;
using UnityEngine;

namespace Creatures.Hero
{
    public class HeroHealthComponent : HealthComponentBase
    {
        [SerializeField] private Animator _animator;

        private static readonly int Health = Animator.StringToHash("Health");
        private InventoryItemDef _poisonKey => GameSession.Instance.QuickInventory.GetCurrentItemDef();
        public override int MaxHealth => (int)GameSession.Instance.StatsModel.GetLevel(StatsType.Health).Value;
        private Cooldown PerkCooldown => GameSession.Instance.PerksModel.CooldownPerk;
        private ShakeCamera _shakeCamera;

        protected override int _currentHealth
        {
            get =>  GameSession.Instance.PlayerData.Health.Value;
            set =>  GameSession.Instance.PlayerData.Health.Value = value;
        }

        private void Start()
        {
            _currentHealth = GameSession.Instance.PlayerData.Health.Value;
            _shakeCamera = FindAnyObjectByType<ShakeCamera>();
        }

        protected override void Damage(int amount)
        {
          //  if (GameSession.Instance.PerksModel.IsActivePerk("Shield") && !PerkCooldown.IsReady()) return;
            base.Damage(amount);
            _shakeCamera?.Shake();
            _animator.SetInteger(Health, _currentHealth);
        }

        protected override void Heal(int amount)
        {
            base.Heal(amount);
            _animator.SetInteger(Health, _currentHealth);
        }

        public void HeroHeal()
        {
            if (GameSession.Instance.PlayerData.Inventory.CountItem(_poisonKey.Name) < 1) return;
            var healAmount = (int)DefsFacade.Instance.Potion.GetItem(_poisonKey.Name).EffectValue;
            GameSession.Instance.PlayerData.Inventory.RemoveItem(_poisonKey.Name, 1);
            Heal(healAmount);
        }
        
    }
}