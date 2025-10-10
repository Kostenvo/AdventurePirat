using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Particles
{
    public class ProbabilityDropComponent :MonoBehaviour
    {
        [SerializeField] private GOForDrop[] _drops;
        [SerializeField] private RandomSpawner _spawner;
        [SerializeField] private int _dropCount;
        [SerializeField] private bool _dropOnStart = false;
        private List<GameObject> _spawned = new List<GameObject>();
        
        public void Spawn()
        {
            CalculateProbability();
            _spawner.Spawn(_spawned.ToArray());
            _spawned.Clear();
        }

        private void Start()
        {
            if (_dropOnStart)
                Spawn();
        }

        private void CalculateProbability()
        {
            var totalDrop = 0;
            while (_dropCount > totalDrop)
            {
                var dropVariation = Random.Range(0, 100);
                foreach (var dropObj in _drops)
                {
                    if (dropObj.probability > dropVariation)
                    {
                        _spawned.Add(dropObj.prefab);
                        totalDrop++;
                    }
                }
            }
        }

        [Serializable]
        public struct GOForDrop
        {
            [Range(1,100)] public int probability;
            public GameObject prefab;
        }

        public void SetCount(int maxCoinsForRemove)
        {
           _dropCount = maxCoinsForRemove;
        }
    }
}