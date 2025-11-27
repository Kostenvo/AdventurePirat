using Creatures;
using UnityEngine;

namespace Animation
{
    public class SinusoidalThrowObject : ThrowObjectBase
    {
        [SerializeField] private float _amplitude;
        [SerializeField] private float _frequency;

        protected override void Start()
        {
            base.Start();
            if (_poolItem != null) return;
            time = 0;  
            yPos = transform.position.y;
        }

        private float yPos;
        private float time;

        private void FixedUpdate()
        {
            var newPositionXposition = yPos + (Mathf.Sin(_frequency * time)) * _amplitude;
            time += Time.fixedDeltaTime;
            _rigidbody.MovePosition(new Vector3(_rigidbody.position.x + _speed * Direction, newPositionXposition));
        }

        public override void StartInPool()
        {
            time = 0;
            base.StartInPool();
            yPos = transform.position.y;
        }
    }
}