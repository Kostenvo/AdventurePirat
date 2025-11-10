using CameraSet;
using UnityEngine;

namespace Camera
{
    public class ShowTargetComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _showTime;
        [SerializeField] private SetTargetForShowCamera _setTargetForShowCamera;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _setTargetForShowCamera = FindAnyObjectByType<SetTargetForShowCamera>();
        }
#endif
        
        public void ShowTarget()
        {
            _setTargetForShowCamera.ShowTarget(_target);
            _setTargetForShowCamera.SwitchAnimator(true);
            Invoke(nameof(MoveBack),_showTime);
        }

        private void MoveBack() => _setTargetForShowCamera.SwitchAnimator(false);
    }
}