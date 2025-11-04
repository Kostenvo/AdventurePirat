using Checkers;
using Particles;
using Sound;
using UnityEngine;

namespace Creatures
{
    public class CreatureMove : MoveBase
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] protected float _jumpSpeed = 8f;
        [SerializeField] private float _jumpDamageValue = 8f;
        [SerializeField] private CheckerSurfaceBase _groundChecker;
        [SerializeField] protected Animator _animator;
        [SerializeField] private AudioListComponent _audioList;
        [SerializeField] private SpawnListComponent _spawnListComponent;

        private const string jumpSpawnKey = "Jump";
        private readonly int moveKey = Animator.StringToHash("Move");
        protected readonly int jumpKey = Animator.StringToHash("VerticalVelocity");
        private readonly int groundCheckKey = Animator.StringToHash("OnGround");
        private readonly string _jampSoundKey = "Jump";
        private Vector2 moveDirection;
       
        protected Rigidbody2D rb;
        protected bool isDamage = false;
        protected bool IsJumping => moveDirection.y > 0;

        protected bool IsGrounded => _groundChecker.CheckSurface();
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

        protected virtual void ChangeStatusOnGround() => DeactivateDamage();


        private void DeactivateDamage() => isDamage = false;
        

        protected virtual float CurrentSpeed() => _moveSpeed;

        private void Move()
        {
            rb.linearVelocityX = moveDirection.x * CurrentSpeed();
            SetDirection();
            _animator.SetBool(groundCheckKey, IsGrounded);
            _animator.SetBool(moveKey, moveDirection.x != 0);
        }


        protected virtual void Jump()
        {
           
        }

        protected void PlayFx()
        {
            _audioList.Play(_jampSoundKey);
            _spawnListComponent.SpawnParticle(ParticleType.Jamp);
        }

        private void SetDirection()
        {
            var currentScale = transform.localScale;
            if (moveDirection.x > 0)
                transform.localScale = new Vector3(1, currentScale.y, currentScale.z);
            else if (moveDirection.x < 0)
                transform.localScale = new Vector3(-1, currentScale.y, currentScale.z);
        }
    }
}