using UnityEngine;

namespace GameObjects
{
    public class DestroyGo : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private RestoreStateComponent _restoreState;

        public void DestroyObject()
        {
            var obj = _gameObject ? _gameObject : gameObject;
            _restoreState = _restoreState ?? GetComponent<RestoreStateComponent>();
            if (_restoreState != null) _restoreState.StoreInSession(); 
            Destroy(obj);
        }
    }
}