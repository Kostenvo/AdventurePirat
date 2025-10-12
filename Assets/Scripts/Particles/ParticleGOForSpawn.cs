using System;
using UnityEngine;
using UnityEngine.Events;

namespace Particles
{
    [Serializable]
    public class ParticleGOForSpawn 
    {
        [SerializeField] private GameObject[] _particleGO;
        [SerializeField] private IventGOForSpawn _spawn;

        public void Spawn()
        {
            _spawn?.Invoke(_particleGO);
        }
    }
    [Serializable]
    public class IventGOForSpawn : UnityEvent<GameObject[]>{} 
}