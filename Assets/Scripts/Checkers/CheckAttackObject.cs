using System;
using Scripts.Creatures;
using Scripts.Creatures.Hero;
using Scripts.GameData;
using Scripts.Interact;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Checkers
{
    public class CheckAttackObject : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMaskForAttack;
        [SerializeField] private int _damage;
        [SerializeField] private float _radius;
        [SerializeField] private Animator _animator; 
        [SerializeField] private RuntimeAnimatorController _armed;
        [SerializeField] private RuntimeAnimatorController _unarmed;
        private Collider2D[] _colliders;
        private readonly int _attackKey = Animator.StringToHash("Attack");
        private GameSession _gameSession;
        private bool _isArmed => _gameSession.PlayerData.IsArmed;

        private void Start()
        {
            _gameSession = FindAnyObjectByType<GameSession>();
            ChangeState();
        }

        public void ArmHero()
        {
            if (_isArmed) return;
            _gameSession.PlayerData.IsArmed = true;
            _animator.runtimeAnimatorController = _armed;
        }
        public void Attack()
        {
            if (!_isArmed) return;
            _animator.SetTrigger(_attackKey);
            _colliders = Physics2D.OverlapCircleAll(transform.position, _radius,_layerMaskForAttack);
            foreach (var collide in _colliders)
            {
                collide.GetComponent<HealthComponentBase>()?.ChangeHealth(-_damage);
            }
        }
        private void ChangeState() => _animator.runtimeAnimatorController = _isArmed? _armed: _unarmed;

        private void OnDrawGizmosSelected()
        {
            Handles.color = CheckerColor.Red;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }

    }
}