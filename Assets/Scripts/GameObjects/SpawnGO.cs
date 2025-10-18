using GameObjects.Extensions;
using UnityEngine;

namespace GameObjects
{
    public class SpawnGo : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _prefab;

        public void Spawn()
        {
            if (!_spawnPoint) _spawnPoint = transform;
            GameObject go = SpawnExtensions.SpawnInParticleContainer(_prefab, _spawnPoint);
            go.SetActive(true);
            go.transform.localScale = transform.lossyScale; 
        }
    }
}