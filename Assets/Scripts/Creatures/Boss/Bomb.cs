using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Creatures.Boss
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private float _detonationTime;
        [SerializeField] private UnityEvent _onExplode;

        private void OnEnable()
        {
            StartCoroutine(Detonate());
        }

        private IEnumerator Detonate()
        {
            yield return new WaitForSeconds(_detonationTime);
            _onExplode?.Invoke();
        }

        public void Explode()
        {
            _onExplode?.Invoke();
        }

    }
}