using UnityEngine;

namespace Scripts.GameObjects
{
    public class DestroyGO : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject;

        public void DestroyObject()
        {
            var obj = _gameObject ? _gameObject : gameObject; 
            Destroy(obj);
        }
    }
}