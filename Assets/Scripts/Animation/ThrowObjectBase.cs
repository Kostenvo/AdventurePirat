using GameObjects;
using UnityEngine;

namespace Animation
{
    public class ThrowObjectBase : MonoBehaviour
    {
        [SerializeField] protected float _speed;
        [SerializeField] protected Rigidbody2D _rigidbody;
        protected float Direction;
        protected PoolItem _poolItem;
      

        protected virtual void Start()
        {
            _poolItem = GetComponent<PoolItem>();
           _rigidbody = _rigidbody ? _rigidbody : GetComponent<Rigidbody2D>();
           if (_poolItem != null) return;
            Direction = transform.lossyScale.x > 0 ? 1 : -1;   
        }

        public virtual void StartInPool()
        {
            _rigidbody = _rigidbody ? _rigidbody : GetComponent<Rigidbody2D>();
            Direction = transform.lossyScale.x > 0 ? 1 : -1;   
        }
    }
}