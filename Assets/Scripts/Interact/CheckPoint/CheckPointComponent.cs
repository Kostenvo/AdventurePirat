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
        private GameSession _gameSession;

        public string CheckPointName => _checkPointName;

        private void Start()
        {
            _spriteAnimationComponent = _spriteAnimationComponent?? GetComponent<SpriteAnimationComponent>();
            _gameSession = _gameSession?? FindFirstObjectByType<GameSession>();
            var isCheked = _gameSession.IsCheckpointChecked(_checkPointName);
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
            _gameSession.AddCheckPoint(_checkPointName);
        }
    }
}