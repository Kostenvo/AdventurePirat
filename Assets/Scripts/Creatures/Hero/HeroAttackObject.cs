using System;
using System.Collections;
using GameData;
using Particles;
using TimeComponent;
using UnityEngine;

namespace Creatures.Hero
{
    public class HeroAttackObject : CheckAttackObjectBase
    {
        [Header("SuperThrow")]
        [SerializeField] private int _superThrowCount;
        [SerializeField] private float _superThrowDelay;
        
        [Header("Cooldowns")] [SerializeField] private Cooldown _attackCooldown;
        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private Cooldown _SuperThrowingCooldown;

        [Header("Animations")] [SerializeField]
        private Animator _animator;

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

        public void EndButtonThrow()
        {
            if (_swordCount <= 0) return;
            if (!_throwCooldown.IsReady()) return;
            if (_SuperThrowingCooldown.IsReady())
            {
                StartCoroutine(SuperTrowingCoroutine());
            }
            else
            {
                Throw();
            }
        }

        private IEnumerator SuperTrowingCoroutine()
        {
            for (int i = 0; i < _superThrowCount; i++)
            {
                if (_swordCount > 1)
                {
                    Throw();
                    yield return new WaitForSeconds(_superThrowDelay);
                }
            }
        }
        
        public void Throw()
        {
            _spawner.SpawnParticle(ParticleType.Throw);
            _gameSession.PlayerData.Inventory.RemoveItem("Sword", 1);
            _animator.SetTrigger(_throwKey);
            ChangeState();
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

        public void ChangeState() => _animator.runtimeAnimatorController = _swordCount > 0 ? _armed : _unarmed;

        public void StartButtonThrow()
        {
            _SuperThrowingCooldown.ResetCooldown();
        }
    }
}