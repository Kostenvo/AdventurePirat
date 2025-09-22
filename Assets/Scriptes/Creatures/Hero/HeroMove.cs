using System;
using Scriptes.Chekers;
using UnityEngine;

namespace Scriptes.Creatures.Hero
{
    public class HeroMove : MoveBase
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float _jampSpeed = 5f;
        [SerializeField] private ChekerSurfaceBase _groundChecker;
        private Vector2 _moveDirection;
        private bool isJamping => _moveDirection.y > 0;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_groundChecker == null)  
                _groundChecker = GetComponentInChildren<ChekerSurfaceBase>();
        }
#endif

        public override void SetDirection(Vector2 dir)
        {
            _moveDirection = dir;
        }

        private void FixedUpdate()
        {
            Jamp();
            Move();
        }

        private void Jamp()
        {
            if (isJamping)
            {
                if (_groundChecker?.CheckSurface() == true)
                    _rb.AddForceY(_jampSpeed, ForceMode2D.Impulse);
            }
            else if (_rb.linearVelocity.y > 0)
            {
                _rb.linearVelocityY = _rb.linearVelocityY / 2;
            }
        }

        private void Move()
        {
            if (_groundChecker?.CheckSurface() == true)
                _rb.linearVelocityX = _moveDirection.x * moveSpeed;
        }
    }
}