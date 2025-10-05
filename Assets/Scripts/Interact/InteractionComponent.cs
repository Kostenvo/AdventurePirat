using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Interact
{
    public class InteractionComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onInteract;

        public void Interact()
        {
            _onInteract?.Invoke();
        }
#if UNITY_EDITOR
        private GameObject[] _interactableObjects;

        private void OnValidate()
        {
            _interactableObjects = _onInteract.ToGameObjects();
        }

        public void OnDrawGizmosSelected()
        {
            foreach (var interactableObject in _interactableObjects)
            {
                if(!interactableObject) return;
                Debug.DrawLine(transform.position, interactableObject.transform.position, Color.green);
            }
        }
#endif
    }
}