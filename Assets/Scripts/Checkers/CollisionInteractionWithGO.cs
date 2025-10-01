using System;
using UnityEngine;
using UnityEngine.Events;

namespace Checkers
{
    public class CollisionInteractionWithGo : MonoBehaviour
    {
        [SerializeField] private GOEvent _onTriggerInteracted;
        [SerializeField] private string _tag;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(_tag))
                _onTriggerInteracted?.Invoke(other.gameObject);
        }
    }
    [Serializable]
    public class GOEvent : UnityEvent<GameObject> { }
}