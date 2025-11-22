using System;
using Creatures.Patrolling;
using UnityEngine;
using UnityEngine.Serialization;

namespace Creatures
{
    public class TentacleAI : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private PlatformPatrol _platformPatrol;
        private Vector2 _moveDirection;

        private void Start() => StartCoroutine(_platformPatrol.DoPatrol());

        private void Update() => SetDirection();

        private void FixedUpdate() => _rb.linearVelocityX = _moveDirection.x * _speed;


        public void SetDirection(Vector2 dir) => _moveDirection = dir;

        private void SetDirection()
        {
            var currentScale = transform.localScale;
            if (_moveDirection.x > 0)
                transform.localScale = new Vector3(1, currentScale.y, currentScale.z);
            else if (_moveDirection.x < 0)
                transform.localScale = new Vector3(-1, currentScale.y, currentScale.z);
        }
    }
}