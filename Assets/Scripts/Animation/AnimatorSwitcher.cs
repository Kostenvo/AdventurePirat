using System;
using UnityEngine;

namespace Animation
{
    public class AnimatorSwitcher : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _animationName;
        [SerializeField] private bool _setOnStart;
        [SerializeField] private bool _active;

        private void Start()
        {
            if (_setOnStart) _animator.SetBool(_animationName, _active);
        }

        public void Switch()
        {
            _active = !_active;
            _animator.SetBool(_animationName, _active);
        }
    }
}