using System;
using UnityEngine;

namespace UI
{
    public class AnimatedWindow : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private static readonly int ShowKey = Animator.StringToHash("Show");
        private static readonly int HideKey = Animator.StringToHash("Hide");

        private void Start()
        {
            _animator ??= GetComponent<Animator>();
            _animator.SetTrigger(ShowKey);
        }


        public void CloseButton()
        {
            _animator?.SetTrigger(HideKey);
        }

        protected virtual void OnClosedAnimation()
        {
            Destroy(gameObject);
        }
    }
}