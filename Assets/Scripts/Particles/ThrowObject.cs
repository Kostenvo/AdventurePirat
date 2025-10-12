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
           
            var force = new Vector3(_speed * Direction, 0, 0);
            _rigidbody.AddForce(force, ForceMode2D.Impulse);
        }
    }
}