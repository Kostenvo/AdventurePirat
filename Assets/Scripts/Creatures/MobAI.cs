using System.Collections;
using Checkers;
using Creatures.Patrolling;
using Particles;
using UnityEngine;

namespace Creatures
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private float _agroDelay;
        [SerializeField] private float _attackDelay;
        [SerializeField] private float _disappearedDelay;
        [SerializeField] private MoveBase _creatureMove;
        [SerializeField] private CheckAttackObjectBase _attack;
        [SerializeField] private Patrol _patrol;
        [SerializeField] private CheckLayerByTrigger _heroInVision;
        [SerializeField] private CheckLayerByTrigger _heroCanAttack;
        [SerializeField] private SpawnListComponent _spawnParticle;
        private bool _isDead = false;

        private IEnumerator _coroutine;

        private void Start()
        {
            StopStartCoroutine(Patrol());
        }

        public void MoveToHero(GameObject target)
        {
            if (_isDead) return;
            StopStartCoroutine(Agro(target));
        }

        public void Death()
        {
            _isDead = true;
            _creatureMove.SetDirection(Vector2.zero);
            StopCoroutine(_coroutine);
        }

        private IEnumerator Agro(GameObject target)
        {
            _creatureMove.SetDirection(HeroDirection(target));
            yield return null;
            _creatureMove.SetDirection(Vector2.zero);
            _spawnParticle.SoawnParticle(ParticleType.Aggro);
            yield return new WaitForSeconds(_agroDelay);
            StopStartCoroutine(GoToHero(target));
        }

        private IEnumerator GoToHero(GameObject target)
        {
            while (_heroInVision._isTouched)
            {
                if (_heroCanAttack._isTouched)
                {
                    _creatureMove.SetDirection(Vector2.zero);
                    _attack.Attack();
                    yield return new WaitForSeconds(_attackDelay);
                }
                else
                {
                    _creatureMove.SetDirection(HeroDirection(target));
                }
                yield return null;
            }

            _creatureMove.SetDirection(Vector2.zero);
            Disappered();
            yield return new WaitForSeconds(_disappearedDelay);
            StopStartCoroutine(Patrol());
        }

        private void Disappered()
        {
            _spawnParticle.SoawnParticle(ParticleType.Disappeared);
        }

        private void StopStartCoroutine(IEnumerator coroutine)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = coroutine;
            StartCoroutine(coroutine);
        }

        private IEnumerator Patrol()
        {
            yield return _patrol.DoPatrol();
        }

        private Vector2 HeroDirection(GameObject target)
        {
            var directionX = (target.transform.position - transform.position).normalized.x;
            // return new Vector2(Mathf.Sign(directionX), 0f);
            return new Vector2(directionX, 0f);
        }
    }
}