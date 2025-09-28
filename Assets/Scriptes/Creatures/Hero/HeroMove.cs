using System;
using Scripts.Checkers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Creatures.Hero
{
    public class HeroMove : MoveBase, IMovable
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpSpeed = 8f;
        [SerializeField] private float jumpDamageValue = 8f;
        [SerializeField] private CheckerSurfaceBase groundChecker;
        [SerializeField] private Animator animator;

        private readonly int _moveKey = Animator.StringToHash("Move");
        private readonly int _jumpKey = Animator.StringToHash("VerticalVelocity");
        private readonly int _groundCheckKey = Animator.StringToHash("OnGround");
        private Rigidbody2D _rb;
        private Vector2 _moveDirection;
        private bool _isDamage = false;
        private bool _isDoubleJump = true;
        private bool IsJumping => _moveDirection.y > 0;

        private bool IsGrounded => groundChecker.CheckSurface();
        private void Awake() => _rb = GetComponent<Rigidbody2D>();

        private void FixedUpdate()
        {
            Jump();
            Move();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (groundChecker == null)
                groundChecker = GetComponentInChildren<CheckerSurfaceBase>();
        }
#endif
        public override void SetDirection(Vector2 dir) => _moveDirection = dir;

        public void SetDamage()
        {
            _isDamage = true;
            if (_rb.linearVelocity.y > 0f) return;
            _rb.AddForceY(jumpDamageValue, ForceMode2D.Impulse);
        }

        private void ChangeStatusOnGround()
        {
            if (IsGrounded)
            {
                DeactivateDamage();
                ActivateDoubleJump();
            }
        }

        private void ActivateDoubleJump() => _isDoubleJump = true;

        private void DeactivateDamage() => _isDamage = false;

        private void Jump()
        {
            float minimumValueForJump = 0.1f;
            if (IsJumping)
            {
                if (!_isDamage && IsGrounded && _rb.linearVelocity.y < minimumValueForJump)
                    _rb.AddForceY(jumpSpeed, ForceMode2D.Impulse);
                else if (_isDoubleJump && _rb.linearVelocity.y < minimumValueForJump)
                {
                    _rb.AddForceY(jumpSpeed, ForceMode2D.Impulse);
                    _isDoubleJump = false;
                }
            }
            else if (!_isDamage && _rb.linearVelocity.y > minimumValueForJump)
            {
                _rb.linearVelocityY = _rb.linearVelocityY / 2;
            }

            ChangeStatusOnGround();
            animator.SetFloat(_jumpKey, _rb.linearVelocity.y);
        }

        private void Move()
        {
            _rb.linearVelocityX = _moveDirection.x * moveSpeed;
            SetDirection();
            animator.SetBool(_groundCheckKey, IsGrounded);
            animator.SetBool(_moveKey, _moveDirection.x != 0);
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