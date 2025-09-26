using UnityEngine;

namespace Scripts.Interact
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _destinationTarget;

        public void Teleport(GameObject target)
        {
           target.transform.position = _destinationTarget.position;     
        }
    }
}