using UnityEngine;

namespace Animation
{
    public class ThrowObjectBase : MonoBehaviour
    {
        [SerializeField] protected float _speed;
        [SerializeField] protected Rigidbody2D _rigidbody;
        protected float Direction;

        protected virtual void Start()
        {
            if (!_rigidbody) _rigidbody = GetComponent<Rigidbody2D>();
            Direction = transform.lossyScale.x > 0 ? 1 : -1;   
        }
    }
}