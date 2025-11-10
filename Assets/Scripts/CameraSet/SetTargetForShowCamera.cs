using Unity.Cinemachine;
using UnityEngine;

namespace CameraSet
{
    public class SetTargetForShowCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _showCamera;
        [SerializeField] private Animator _animator;
        private static readonly int Show = Animator.StringToHash("Show");

        public void ShowTarget(Transform target)
        {
            _showCamera.transform.position =
                new Vector3(target.position.x, target.position.y, _showCamera.transform.position.z);
        }

        public void SwitchAnimator(bool state)
        {
            _animator.SetBool(Show, state);
        }
    }
}