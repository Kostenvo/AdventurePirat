using System;
using UnityEngine;

namespace Background
{
    public class FollowBackground : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private float _deltaPosition;
        private float _startPosition;

        private void Start()
        {
            _startPosition = _target.transform.position.x;
            transform.position = new Vector3(_target.transform.position.x, transform.position.y, transform.position.z);
        }

        private void LateUpdate()
        {
            var newPosition = (_target.transform.position.x - _startPosition) * _deltaPosition;
            transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
        }
    }
}