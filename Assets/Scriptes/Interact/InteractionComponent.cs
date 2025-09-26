using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Interact
{
    public class InteractionComponent :MonoBehaviour
    {
        [SerializeField] private UnityEvent onInteract;
        public void Interact()
        {
            onInteract?.Invoke();
        }
    }
}