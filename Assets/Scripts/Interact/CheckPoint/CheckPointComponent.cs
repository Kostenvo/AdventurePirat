using Animation;
using GameData;
using GameObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Interact.CheckPoint
{
    [UnityEngine.RequireComponent(typeof(SpriteAnimationComponent))]
    public class CheckPointComponent : MonoBehaviour
    {
        [SerializeField] private string _checkPointName;
        [SerializeField] private SpriteAnimationComponent _spriteAnimationComponent;
        [SerializeField] private SpawnGo _spawnGo;
        [SerializeField] private UnityEvent _setChecked;
        [SerializeField] private UnityEvent _setUnchecked;

        public string CheckPointName => _checkPointName;

        private void Start()
        {
    
            var isCheked = GameSession.Instance.IsCheckpointChecked(_checkPointName);
             SetStatus(isCheked);
        }

        private void SetStatus(bool status)
        {
            if(status) _setChecked?.Invoke();
            else _setUnchecked?.Invoke();
        }

        public void SpawnHero()
        {
            _spawnGo.Spawn();
        }


        public void Check()
        {
            GameSession.Instance.AddCheckPoint(_checkPointName);
        }
    }
}