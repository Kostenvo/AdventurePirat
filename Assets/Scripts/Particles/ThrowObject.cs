using System;
using Animation;
using UnityEngine;

namespace Creatures
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ThrowObject : ThrowObjectBase
    {
        protected override void Start()
        {
            base.Start();
            if (_poolItem != null) return;
            var force = new Vector3(_speed * Direction, 0, 0);
            _rigidbody.AddForce(force, ForceMode2D.Impulse);
        }

        public override void StartInPool()
        {
            base.StartInPool();
            var force = new Vector3(_speed * Direction, 0, 0);
            _rigidbody.AddForce(force, ForceMode2D.Impulse);
        }
    }
}