using Animation;
using UnityEngine;

namespace Particles
{
    public class ThrowDirectional : ThrowObjectBase
    {
        protected override void Start()
        {
            base.Start();
        }

        public void SpawnTowards(Vector3 direction)
        {
            // Здесь тоже используем обычную Unity-проверку через == null, а не оператор ??=
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody2D>();
            
            var force = direction* _speed;
            _rigidbody.AddForce(force, ForceMode2D.Impulse);
        }
    }
}