using System.Collections;
using UnityEngine;

namespace Creatures.Boss
{
    public class WaterFall : MonoBehaviour
    {
        [SerializeField] public Animator _animator;
        [SerializeField] public float _timeToWaterFall;
        private static readonly int Up = Animator.StringToHash("Up");
        private Coroutine _waterFallCoroutine;

        public void WaterFallAnimation()
        {
            if (_waterFallCoroutine == null)
                _waterFallCoroutine = StartCoroutine(WaterDowm());
        }

        private IEnumerator WaterDowm()
        {
            _animator.SetBool(Up, true);
            yield return new WaitForSeconds(_timeToWaterFall);
            _animator.SetBool(Up, false);
            _waterFallCoroutine = null;
        }
    }
}