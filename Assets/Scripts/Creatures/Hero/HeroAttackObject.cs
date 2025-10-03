using Checkers;
using GameData;
using UnityEngine;

namespace Creatures.Hero
{
    public class HeroAttackObject : CheckAttackObjectBase
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private RuntimeAnimatorController _armed;
        [SerializeField] private RuntimeAnimatorController _unarmed;
        private readonly int _attackKey = Animator.StringToHash("Attack");
        private GameSession _gameSession;
        private bool _isArmed => _gameSession.PlayerData.IsArmed;

        private void Start()
        {
            _gameSession = FindAnyObjectByType<GameSession>();
            ChangeState();
        }

        public override void Attack()
        {
            if (!_isArmed) return;
            _animator.SetTrigger(_attackKey);
            base.Attack();
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