using System.Collections;
using Scripts.Creatures;
using UnityEngine;

namespace Creatures.Hero
{
    public class ImmuneAfterHit : MonoBehaviour
    {
        [SerializeField] private HealthComponentBase _health;
        [SerializeField] private float _immuneTime;
        private Coroutine _immuneCoroutine;


        public void SetImmune()
        {
            _immuneCoroutine = StartCoroutine(ChangeImmuneCoroutine());
        }

        private IEnumerator ChangeImmuneCoroutine()
        {
            _health.Lock.AddLock(this);
            yield return new WaitForSeconds(_immuneTime);
            _health.Lock.RemoveLock(this);
            _immuneCoroutine = null;
        }
    }
}