using System;
using Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Checkers
{
    public class CollisionInteractionWithGo : MonoBehaviour
    {
        [SerializeField] private GOEvent _onCollisionInteracted;
        [SerializeField] private LayerMask _layerMask = ~0;
        [SerializeField] private string _tag;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!string.IsNullOrEmpty(_tag) && !other.gameObject.CompareTag(_tag)) return;
            if (!other.gameObject.IsInLayer(_layerMask)) return;
            _onCollisionInteracted?.Invoke(other.gameObject);
        }
    }

    [Serializable]
    public class GOEvent : UnityEvent<GameObject>
    {
    }
}