using Interact;
using UnityEditor;
using UnityEngine;

namespace Checkers
{
    public class CheckInteractableObject : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMaskForInteract;
        [SerializeField] private float _radius;
        private Collider2D[] _colliders;
        
        public void Interact()
        {
            _colliders = Physics2D.OverlapCircleAll(transform.position, _radius,_layerMaskForInteract);
            foreach (var collide in _colliders)
            {
                collide.GetComponent<InteractionComponent>()?.Interact();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Handles.color = CheckerColor.Green;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
    }
}