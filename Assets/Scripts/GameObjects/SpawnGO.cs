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
            GameObject go = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);
            go.transform.localScale = transform.lossyScale;
        }
    }
}