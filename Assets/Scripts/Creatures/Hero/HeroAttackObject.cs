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
        private bool _isArmed => _gameSession.PlayerData.IsArmed;

        private void Start()
        {
            _gameSession = FindAnyObjectByType<GameSession>();
            ChangeState();
        }

        public void Throw()
        {
            if (!_isArmed) return;
            if (!_throwCooldown.IsReady()) return;
            _spawner.SoawnParticle(ParticleType.Throw);
            _animator.SetTrigger(_throwKey);
            _throwCooldown.ResetCooldown();
        }

        public override void Attack()
        {
            if (!_isArmed) return;
            if(!_attackCooldown.IsReady()) return;
            _animator.SetTrigger(_attackKey);
            _spawner.SoawnParticle(ParticleType.Attack);
            base.Attack();
            _attackCooldown.ResetCooldown();
        }

        public void ArmHero()
        {
            if (_isArmed) return;
            _gameSession.PlayerData.IsArmed = true;
            _animator.runtimeAnimatorController = _armed;
        }

        private void ChangeState() => _animator.runtimeAnimatorController = _isArmed? _armed: _unarmed;
    }
}