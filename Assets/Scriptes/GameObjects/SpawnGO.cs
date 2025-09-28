using UnityEngine;

namespace Scripts.GameObjects
{
    public class SpawnGO : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject _prefab;

        public void Spawn()
        {
            GameObject go = Instantiate(_prefab, transform.position, Quaternion.identity);
            go.transform.localScale = transform.lossyScale;
        }
    }
}