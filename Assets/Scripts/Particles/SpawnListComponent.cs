using System;
using GameObjects;
using UnityEngine;

namespace Particles
{
    public class SpawnListComponent : MonoBehaviour
    {
        [SerializeField] private ParciesForSpawn[] _parciesForSpawn;

        public void SoawnParticle(ParticleType particleType)
        {
            if (_parciesForSpawn != null && _parciesForSpawn.Length > 0)
            {
                foreach (var particle in _parciesForSpawn)
                {
                    if (particle.ParticleType == particleType)
                    {
                        particle.Spawner.Spawn();
                    }
                }
            }
            else
            {
                Debug.Log("No particle named: " + particleType);
            }
        }

        public void SpawnAll()
        {
            foreach (var spawner in _parciesForSpawn)
            {
                spawner.Spawner.Spawn();
            }
        }

        [Serializable]
        public struct ParciesForSpawn
        {
            public ParticleType ParticleType;
            public SpawnGo Spawner;
        }
    }

    public enum ParticleType
    {
        Jamp,
        Plopped,
        FootStep,
        Aggro,
        Disappeared,
        Throw,
        Attack,
        Non
    }
}