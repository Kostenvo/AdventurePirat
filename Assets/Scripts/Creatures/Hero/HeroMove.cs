using System;
using Definitions;
using GameData;
using Subscribe;
using TimeComponent;
using UnityEngine;

namespace Creatures.Hero
{
    public class HeroMove : CreatureMove
    {
        [SerializeField] private Cooldown _cooldown;
        private GameSession _gameSession;
        private PotionDef _currentPotionDef;
        private bool _isDoubleJump = true;
        private ComposideDisposible _trash = new ComposideDisposible();
        private Cooldown PerkCooldown => _gameSession.PerksModel.CooldownPerk;

        private void ActivateDoubleJump() => _isDoubleJump = true;

        private void Start()
        {
            _gameSession = FindAnyObjectByType<GameSession>();
            _trash.Retain( _gameSession.StatsModel.Subscribe(UpdateSpeed));
        }

        private void UpdateSpeed()
        {
            if (_gameSession.StatsModel.SelectedStats.Value == StatsType.Speed)
                _moveSpeed = _gameSession.StatsModel.GetLevel(StatsType.Speed).Value;
        }

        protected override void ChangeStatusOnGround()
        {
            if (IsGrounded)
            {
                base.ChangeStatusOnGround();
                ActivateDoubleJump();
            }
        }

        protected override float CurrentSpeed() => _moveSpeed + IncrementalSpeed();

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
                _gameSession.PlayerData.Inventory.RemoveItem(_currentPotionDef.Name, 1);
            }
            else
            {
                _cooldown.ResetCooldown();
                _gameSession.PlayerData.Inventory.RemoveItem(_currentPotionDef.Name, 1);
            }
        }

        protected override void Jump()
        {
            float minimumValueForJump = 0.1f;
            if (IsJumping)
            {
                if (!isDamage && IsGrounded && rb.linearVelocity.y < minimumValueForJump)
                {
                    rb.AddForceY(_jumpSpeed, ForceMode2D.Impulse);
                    PlayFx();
                }
                else if (_gameSession.PerksModel.IsActivePerk("SuppreJamp") && PerkCooldown.IsReady() && _isDoubleJump && rb.linearVelocity.y < minimumValueForJump)
                {
                    PlayFx();
                    rb.AddForceY(_jumpSpeed, ForceMode2D.Impulse);
                    _isDoubleJump = false;
                    PerkCooldown.ResetCooldown();
                }
            }
            else if (!isDamage && rb.linearVelocity.y > minimumValueForJump)
            {
                rb.linearVelocityY = rb.linearVelocityY / 2;
            }

            ChangeStatusOnGround();
            _animator.SetFloat(jumpKey, rb.linearVelocity.y);
        }

        private void ChangeDef(InventoryItemDef def)
        {
            var pDef = DefsFacade.Instance.Potion.GetItem(def.Name);
            _currentPotionDef = pDef;
            _cooldown = new Cooldown(_currentPotionDef.CooldownValue);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}