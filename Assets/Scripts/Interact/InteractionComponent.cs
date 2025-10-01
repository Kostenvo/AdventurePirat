using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Interact
{
    public class InteractionComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onInteract;

        public void Interact()
        {
            _onInteract?.Invoke();
        }
    }
}