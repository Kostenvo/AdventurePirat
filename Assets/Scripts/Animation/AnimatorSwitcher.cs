using UnityEngine;

namespace Animation
{
    public class AnimatorSwitcher : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _animationName;
        private bool _active;

        public void Switch()
        {
            _active = !_active;
            _animator.SetBool(_animationName, _active);
        }
    }
}