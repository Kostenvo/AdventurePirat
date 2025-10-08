using Extensions;
using Particles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scriptes.Particles
{
    public class PloppedComponent :MonoBehaviour
    {
        [SerializeField] private int _maxHeightForPlopped;
        [SerializeField] private LayerMask _layerMask;
        [FormerlySerializedAs("_particleSpawner")] [SerializeField] private SpawnListComponent _spawnListComponent;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.IsInLayer(_layerMask)) return;
            var contact = other.contacts[0];
            if (contact.relativeVelocity.y > _maxHeightForPlopped)
            {
                _spawnListComponent.SpawnParticle(ParticleType.Plopped); 
            }
        }
    }
}