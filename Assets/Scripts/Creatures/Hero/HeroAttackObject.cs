using System;
using Checkers;
using GameData;
using GameObjects;
using Particles;
using TimeComponent;
using UnityEngine;

namespace Creatures.Hero
{
    public class HeroAttackObject : CheckAttackObjectBase
    {
        [SerializeField] private Cooldown _attackCooldown;
        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private Animator _animator;
        [SerializeField] private RuntimeAnimatorController _armed;
        [SerializeField] private RuntimeAnimatorController _unarmed;
        [SerializeField] private SpawnListComponent _spawner;
        private readonly int _attackKey = Animator.StringToHash("Attack");
        private readonly int _throwKey = Animator.StringToHash("Throw");
        private GameSession _gameSession;
        private int _swordCount => _gameSession.PlayerData.Inventory.CountItem("Sword");

        private void Start()
        {
            _gameSession = FindAnyObjectByType<GameSession>();
            ChangeState();
        }

        public void Throw()
        {
            if (_swordCount <= 0) return;
            if (!_throwCooldown.IsReady()) return;
            _spawner.SpawnParticle(ParticleType.Throw);
            _animator.SetTrigger(_throwKey);
            _throwCooldown.ResetCooldown();
        }

        public override void Attack()
        {
            if (_swordCount <= 0) return;
            if (!_attackCooldown.IsReady()) return;
            _animator.SetTrigger(_attackKey);
            _spawner.SpawnParticle(ParticleType.Attack);
            base.Attack();
            _attackCooldown.ResetCooldown();
        }

        public void ArmHero()
        {
            if (_swordCount <= 0) return;
            _animator.runtimeAnimatorController = _armed;
        }

        public void ChangeState() => _animator.runtimeAnimatorController = _swordCount > 0 ? _armed : _unarmed;
    }
}