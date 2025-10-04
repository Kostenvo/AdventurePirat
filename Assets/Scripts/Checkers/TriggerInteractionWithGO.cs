using Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Checkers
{
    public class TriggerInteractionWithGo : MonoBehaviour
    {
        [SerializeField] private GOEvent _onTriggerEnterWithGoEvent;
        [SerializeField] private LayerMask _layerMask = ~0;
        [SerializeField] private string _tag;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!string.IsNullOrEmpty(_tag) && !other.gameObject.CompareTag(_tag)) return;
            if (!other.gameObject.IsInLayer(_layerMask)) return;
            _onTriggerEnterWithGoEvent?.Invoke(other.gameObject);
        }
    }
}