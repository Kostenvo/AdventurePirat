using Scriptes.Particles;
using UnityEngine;

namespace Scripts.Checkers
{
    public class PloppedComponent :MonoBehaviour
    {
        [SerializeField] private int _maxHeightForPlopped;
        [SerializeField] private ParticleSpawner _particleSpawner;

        private void OnCollisionEnter2D(Collision2D other)
        {
            var contact = other.contacts[0];
            if (contact.relativeVelocity.y > _maxHeightForPlopped)
            {
                _particleSpawner.SoawnParticle(ParticleType.Plopped);
            }
        }
    }
}