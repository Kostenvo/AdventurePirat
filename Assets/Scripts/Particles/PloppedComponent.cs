using Scriptes.Extensions;
using Scripts.GameObjects;
using UnityEngine;

namespace Scriptes.Particles
{
    public class PloppedComponent :MonoBehaviour
    {
        [SerializeField] private int _maxHeightForPlopped;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private ParticleSpawner _particleSpawner;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.IsInLayer(_layerMask)) return;
            var contact = other.contacts[0];
            if (contact.relativeVelocity.y > _maxHeightForPlopped)
            {
                _particleSpawner.SoawnParticle(ParticleType.Plopped); 
            }
        }
    }
}