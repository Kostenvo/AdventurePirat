using System;
using Checkers;
using Particles;
using TimeComponent;
using UnityEngine;

namespace Creatures
{
    public class ShootingTrapAI : MonoBehaviour
    {
        [Header("MeleeAttack")]
        [SerializeField] private CheckLayerByTrigger _checkMeleeAttack;
        [SerializeField] private Cooldown _meleeCooldown;
        [SerializeField] private CheckAttackObjectBase _attack;

        [Header("RangeAttack")]
        [SerializeField] private CheckLayerByTrigger _checkRangeAttack;
        [SerializeField] private Cooldown _rangeCooldown;
        
        [SerializeField] private SpawnListComponent _spawnList;
        
        [SerializeField] private Animator _animator;

        private static readonly int MeleeKey = Animator.StringToHash("Melee");
        private static readonly int RangeKey = Animator.StringToHash("Range");
        
        private GameObject _heroTarget;

        private void Update()
        {
            if (_checkMeleeAttack._isTouched)
            {
                if (_meleeCooldown.IsReady())
                {
                    LookAtHero();
                    _meleeCooldown.ResetCooldown();
                    _animator.SetTrigger(MeleeKey);
                    return;
                }
            }else if (_checkRangeAttack._isTouched)
            {
                if (_rangeCooldown.IsReady())
                {
                    LookAtHero();
                    _rangeCooldown.ResetCooldown();
                    _animator.SetTrigger(RangeKey);
                    return;
                }
            }
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
               transform.localScale = new Vector3(position, 1, 1);
            }
        }
        

        private void DoRange() => _spawnList.SpawnParticle(ParticleType.Throw);

        private void DoMelee() => _attack.Attack();
    }
}