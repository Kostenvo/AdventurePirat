using System;
using Checkers;
using GameObjects;
using Particles;
using TimeComponent;
using UnityEngine;
using UnityEngine.Events;

namespace Creatures
{
    public class ShootingTrapAI : MonoBehaviour
    {
        [SerializeField] private CheckLayerByTrigger _heroCheck;
        [SerializeField] private Cooldown _cooldown;
        [SerializeField] private UnityEvent _onAttack;
        [SerializeField] private ParticleGOForSpawn _particleGO;
        private GameObject _heroTarget;

        public bool IsTouchedHero => _heroCheck._isTouched;

        private void Update()
        {
            if (IsTouchedHero && _cooldown.IsReady())
            {
                _cooldown.ResetCooldown();
                Attack();
            }
        }

        public void Attack()
        {
            LookAtHero();
            _onAttack?.Invoke(); 
        }

        public void SetTarget(GameObject target)
        {
            _heroTarget = target;
            LookAtHero();
        }

        private void LookAtHero()
        {
            if (_heroTarget)
            {
                var position = _heroTarget.transform.position.x - transform.position.x > 0 ? 1 : -1;
                var currentScale = transform.localScale;
                transform.localScale = new Vector3(position, currentScale.y, currentScale.z);
            }
        }

        [ContextMenu("Spawn")]
        public void Death()
        {
            _particleGO.Spawn();
        }
    }
}