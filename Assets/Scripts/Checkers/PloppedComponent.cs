using Particles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Checkers
{
    public class PloppedComponent :MonoBehaviour
    {
        [SerializeField] private int _maxHeightForPlopped;
        [FormerlySerializedAs("_particleSpawner")] [SerializeField] private SpawnListComponent _spawnListComponent;

        private void OnCollisionEnter2D(Collision2D other)
        {
            var contact = other.contacts[0];
            if (contact.relativeVelocity.y > _maxHeightForPlopped)
            {
                _spawnListComponent.SpawnParticle(ParticleType.Plopped);
            }
        }
    }
}