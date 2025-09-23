using System;
using Scriptes.Chekers;
using UnityEngine;

namespace Scriptes.Creatures.Hero
{
    public class HeroMove : MoveBase, IMovable
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float _jampSpeed = 5f;
        [SerializeField] private ChekerSurfaceBase _groundChecker;
        [SerializeField] private Animator _animator;
        private Vector2 _moveDirection;
        private bool isJamping => _moveDirection.y > 0;
        private Rigidbody2D _rb;
        private int _moveKey = Animator.StringToHash("Move");
        private int _jumpKey = Animator.StringToHash("VerticalVelosity");
        private int _groundCheckKey = Animator.StringToHash("OnGround");
        private bool _groundCheck => _groundChecker.CheckSurface();


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
                if (_groundCheck == true && _rb.linearVelocity.y < 0.1f)
                    _rb.AddForceY(_jampSpeed, ForceMode2D.Impulse);
            }
            else if (_rb.linearVelocity.y > 0)
            {
                _rb.linearVelocityY = _rb.linearVelocityY / 2;
            }

            _animator.SetFloat(_jumpKey, _rb.linearVelocity.y);
        }

        private void Move()
        {
            _rb.linearVelocityX = _moveDirection.x * moveSpeed;
            SetDirection();
            _animator.SetBool(_groundCheckKey, _groundCheck);
            _animator.SetBool(_moveKey, _moveDirection.x != 0);
        }

        private void SetDirection()
        {
            if (_moveDirection.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (_moveDirection.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}