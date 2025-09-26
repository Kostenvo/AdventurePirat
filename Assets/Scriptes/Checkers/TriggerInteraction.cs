using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Checkers
{
    public class TriggerInteraction : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onTriggerInteracted;
        [SerializeField] private string _tag;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(_tag))
                _onTriggerInteracted?.Invoke();
        }
    }
}