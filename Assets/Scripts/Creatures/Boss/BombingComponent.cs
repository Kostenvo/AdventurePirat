using System;
using System.Collections;
using GameObjects;
using UnityEngine;

namespace Creatures.Boss
{
    public class BombingComponent: MonoBehaviour
    {
        [SerializeField] private BombingSequence[] _spawnConfig;
        [SerializeField] private GameObject[] _platforms;
       
        
        [ContextMenu("Spawn")]
        public void Spawn()
        {
            StartCoroutine(Bombing());
        }

        private IEnumerator Bombing()
        {
            foreach (var platform in _platforms)
            {
                platform.SetActive(false);
            }

            foreach (var spawner in _spawnConfig)
            {
                foreach (var go in spawner.SpawnGOs)
                {
                    go.Spawn();
                    yield return new WaitForSeconds(spawner.SpawnInterval);
                }
            }
            foreach (var platform in _platforms)
            {
                platform.SetActive(true);
            }
        }
        
        [Serializable]
        private struct BombingSequence
        {
            [SerializeField] private SpawnGo[] _spawnGOs;
            [SerializeField] private float _spawnInterval;

            public float SpawnInterval => _spawnInterval;

            public SpawnGo[] SpawnGOs => _spawnGOs;
        }
    }
}