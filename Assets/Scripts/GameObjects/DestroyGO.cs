using UnityEngine;

namespace GameObjects
{
    public class DestroyGo : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject;

        public void DestroyObject()
        {
            var obj = _gameObject ? _gameObject : gameObject; 
            Destroy(obj);
        }
    }
}