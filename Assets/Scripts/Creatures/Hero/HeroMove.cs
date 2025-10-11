using Checkers;
using Creatures;
using Particles;
using Scriptes.Particles;
using Sound;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Creatures.Hero
{
    public class HeroMove : MoveBase
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _jumpSpeed = 8f;
        [SerializeField] private float _jumpDamageValue = 8f;
        [SerializeField] private CheckerSurfaceBase _groundChecker;
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioListComponent _audioList;
        [SerializeField] private SpawnListComponent _spawnListComponent;

        private const string jumpSpawnKey = "Jump";
        private readonly int moveKey = Animator.StringToHash("Move");
        private readonly int jumpKey = Animator.StringToHash("VerticalVelocity");
        private readonly int groundCheckKey = Animator.StringToHash("OnGround");
        private readonly string _jampSoundKey = "Jump";
        private Rigidbody2D rb;
        private Vector2 moveDirection;
        private bool isDamage = false;
        private bool isDoubleJump = true;
        private bool IsJumping => moveDirection.y > 0;

        private bool IsGrounded => _groundChecker.CheckSurface();
        private void Awake() => rb = GetComponent<Rigidbody2D>();

        private void FixedUpdate()
        {
            Jump();
            Move();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_groundChecker == null)
                _groundChecker = GetComponentInChildren<CheckerSurfaceBase>();
        }
#endif
        public override void SetDirection(Vector2 dir) => moveDirection = dir;

        public void SetDamage()
        {
            isDamage = true;
            if (rb.linearVelocity.y > 0f) return;
            rb.AddForceY(_jumpDamageValue, ForceMode2D.Impulse);
        }

        private void ChangeStatusOnGround()
        {
            if (IsGrounded)
            {
                DeactivateDamage();
                ActivateDoubleJump();
            }
        }

        private void ActivateDoubleJump() => isDoubleJump = true;

        private void DeactivateDamage() => isDamage = false;


        private void Jump()
        {
            float minimumValueForJump = 0.1f;
            if (IsJumping)
            {
                if (!isDamage && IsGrounded && rb.linearVelocity.y < minimumValueForJump)
                {
                    rb.AddForceY(_jumpSpeed, ForceMode2D.Impulse);
                    PlayFx();
                }
                else if (isDoubleJump && rb.linearVelocity.y < minimumValueForJump)
                {
                    PlayFx();
                    rb.AddForceY(_jumpSpeed, ForceMode2D.Impulse);
                    isDoubleJump = false;
                }
            }
            else if (!isDamage && rb.linearVelocity.y > minimumValueForJump)
            {
                rb.linearVelocityY = rb.linearVelocityY / 2;
            }

            ChangeStatusOnGround();
            _animator.SetFloat(jumpKey, rb.linearVelocity.y);
        }

        private void PlayFx()
        {
            _audioList.Play(_jampSoundKey);
            _spawnListComponent.SpawnParticle(ParticleType.Jamp);
        }

        private void Move()
        {
            rb.linearVelocityX = moveDirection.x * _moveSpeed;
            SetDirection();
            _animator.SetBool(groundCheckKey, IsGrounded);
            _animator.SetBool(moveKey, moveDirection.x != 0);
        }

        private void SetDirection()
        {
            if (moveDirection.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (moveDirection.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}