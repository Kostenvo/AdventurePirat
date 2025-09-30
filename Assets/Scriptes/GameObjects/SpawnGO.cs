using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.GameObjects
{
    public class SpawnGO : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _prefab;

        public void Spawn()
        {
            GameObject go = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);
            go.transform.localScale = transform.lossyScale;
        }
    }
}