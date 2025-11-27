using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GameObjects
{
    public class Pool : MonoBehaviour
    {
        private Dictionary<int, Queue<PoolItem>> _pools = new Dictionary<int, Queue<PoolItem>>();
        private static string _spawnPoolObjectContainerName = "###POOL ITEMS###";
        private static Pool _instance;

        public static Pool Instance
        {
            get
            {
                if (_instance == null)
                {
                    var container = new GameObject(_spawnPoolObjectContainerName).AddComponent<Pool>();
                    _instance = container;
                    return _instance;
                }

                return _instance;
            }
        }

        public void Retain(PoolItem poolItem)
        {
            var pool = GetPool(poolItem.PoolID);
            pool.Enqueue(poolItem);
        }

        public GameObject Release(PoolItem poolItem, Transform position)
        {
            var poolId = poolItem.gameObject.GetInstanceID();
            var pool = GetPool(poolId);
            if (pool.Count > 0)
            {
                poolItem = pool.Dequeue();
            }
            else
            {
                poolItem = Instantiate(poolItem.gameObject, _instance.transform).GetComponent<PoolItem>();
            }

            poolItem.transform.position = position.position;
            poolItem.transform.localScale = position.transform.lossyScale;
            poolItem.Relese(poolId);
            return poolItem.gameObject;
        }


        private Queue<PoolItem> GetPool(int poolID)
        {
            if (_pools.ContainsKey(poolID)) return _pools[poolID];
            var pool = new Queue<PoolItem>();
            _pools.Add(poolID, pool);
            return pool;
        }
    }
}