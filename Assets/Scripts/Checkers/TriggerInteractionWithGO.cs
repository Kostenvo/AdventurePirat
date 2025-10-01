using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Scripts.Checkers
{
    public class TriggerInteractionWithGO : MonoBehaviour
    {
        [SerializeField] private GOEvent _onTriggerEnterWithGOEvent;
        [SerializeField] private string _tag;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(_tag)) return;
            _onTriggerEnterWithGOEvent?.Invoke(other.gameObject);
        }
    }
}