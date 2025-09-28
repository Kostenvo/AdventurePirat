using System;
using UnityEngine;

namespace Scripts.GameObjects
{
    public class ParticleSpawner : MonoBehaviour
    {
        [SerializeField] private ParciesForSpawn[] _parciesForSpawn;

        public void ApplyParticle(string name)
        {
            if (!string.IsNullOrEmpty(name) && _parciesForSpawn != null && _parciesForSpawn.Length > 0)
            {
                foreach (var particle in _parciesForSpawn)
                {
                    if (particle.ParticleName.Contains(name))
                    {
                        particle.Spawner.Spawn();
                    }
                }
            }
            else
            {
                Debug.LogWarning("No particle named: " + name);
            }

        }
        
        [Serializable]
        public struct ParciesForSpawn
        {
            public string ParticleName;
            public SpawnGO Spawner;
        }
    }
}