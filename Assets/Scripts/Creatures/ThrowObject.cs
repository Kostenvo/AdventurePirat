using System;
using UnityEngine;

namespace Creatures
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ThrowObject : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rigidbody;

        private void Start()
        {
            if (!_rigidbody) _rigidbody = GetComponent<Rigidbody2D>();
            var direction = transform.lossyScale.x > 0 ? 1 : -1;
            var force = new Vector3(_speed * direction, 0, 0);
            _rigidbody.AddForce(force, ForceMode2D.Impulse);
        }
    }
}