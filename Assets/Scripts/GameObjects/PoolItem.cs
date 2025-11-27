using UnityEngine;
using UnityEngine.Events;

namespace GameObjects
{
    public class PoolItem : MonoBehaviour
    {
        [SerializeField] public UnityEvent _onRelease;
        private int _poolID;

        public int PoolID => _poolID;

        public void Retain()
        {
            Pool.Instance.Retain(this);
            gameObject.SetActive(false);
        }

        public void Relese(int poolID)
        {
            _poolID = poolID;
            gameObject.SetActive(true);
            _onRelease?.Invoke();
        }
    }
}