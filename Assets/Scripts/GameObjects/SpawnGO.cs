using Definitions;
using GameObjects.Extensions;
using UnityEngine;

namespace GameObjects
{
    public class SpawnGo : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private bool _isPoolObject;

        public GameObject SpawnGO()
        {
            if (!_spawnPoint) _spawnPoint = transform;
            GameObject go;
            PoolItem poolItem;
            if (_isPoolObject && (poolItem = _prefab.GetComponent<PoolItem>()) != null)
            {
                go = Pool.Instance.Release(poolItem , _spawnPoint);
            }
            else
            {
                go = SpawnExtensions.SpawnInParticleContainer(_prefab, _spawnPoint);
                go.transform.localScale = transform.lossyScale;
                go.SetActive(true);
            }

            return go;
        }

        public void Spawn()
        {
            SpawnGO();
        }


        public void SetPrefabe(GameObject throwableTrowItem)
        {
            _prefab = throwableTrowItem;
        }
    }
}