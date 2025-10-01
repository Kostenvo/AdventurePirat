using UnityEngine;
using UnityEngine.Serialization;

namespace Checkers
{
    public class TriggerInteractionWithGo : MonoBehaviour
    {
        [FormerlySerializedAs("_onTriggerEnterWithGOEvent")] [SerializeField] private GOEvent _onTriggerEnterWithGoEvent;
        [SerializeField] private string _tag;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(_tag)) return;
            _onTriggerEnterWithGoEvent?.Invoke(other.gameObject);
        }
    }
}