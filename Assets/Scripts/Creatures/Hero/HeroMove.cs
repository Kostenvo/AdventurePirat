using System;
using Definitions;
using GameData;
using TimeComponent;
using UnityEngine;

namespace Creatures.Hero
{
    public class HeroMove : CreatureMove
    {
        [SerializeField] private Cooldown _cooldown;
        private GameSession _gameSession;
        private PotionDef _currentPotionDef;

        private void Start()
        {
            _gameSession = FindAnyObjectByType<GameSession>();
        }

        protected override float CurrentSpeed() => base.CurrentSpeed() + IncrementalSpeed();

        private float IncrementalSpeed()
        {
            if (_cooldown.IsReady()) return 0;
            else return _currentPotionDef.EffectValue;
        }

        public void SpeedUpPosion()
        {
            var def = _gameSession.QuickInventory.GetCurrentItemDef();
            if (string.IsNullOrEmpty(_currentPotionDef.Name) || !_currentPotionDef.Name.Contains(def.Name))
                ChangeDef(def);
            if (!_cooldown.IsReady())
            {
                _cooldown.AddTimeCooldown();
                _gameSession.PlayerData.Inventory.RemoveItem(_currentPotionDef.Name,1);
            }
            else
            {
                _cooldown.ResetCooldown();
                _gameSession.PlayerData.Inventory.RemoveItem(_currentPotionDef.Name,1);
            }
        }

        private void ChangeDef(InventoryItemDef def)
        {
            var pDef = DefsFacade.Instance.Potion.GetItem(def.Name);
            _currentPotionDef = pDef;
            _cooldown = new Cooldown(_currentPotionDef.CooldownValue);
        }
    }
}