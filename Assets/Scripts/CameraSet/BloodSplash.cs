using System;
using Definitions;
using GameData;
using Subscribe;
using TMPro;
using UnityEngine;

namespace CameraSet
{
    public class BloodSplash : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _transform;
        private ComposideDisposible _trash = new ComposideDisposible();
        private Vector3 _deltaScale;
        private GameSession _gameSession;
        private readonly int _healthKey = Animator.StringToHash("Health");

        private void Start()
        {
            _deltaScale = _transform.localScale - Vector3.one;
            _gameSession = FindFirstObjectByType<GameSession>();
            _trash.Retain(_gameSession.PlayerData.Health.SubscribeAndInvoke(SplashChange));
        }

        private void SplashChange(int newvalue, int oldvalue)
        {
            var maxHealth = _gameSession.StatsModel.GetLevel(StatsType.Health).Value;
            var deltaHealth = (float)newvalue / maxHealth;
            _animator.SetFloat(_healthKey, deltaHealth);
            var currentDelta = Mathf.Max(0, deltaHealth - 0.3f);
            _transform.localScale = _deltaScale * currentDelta + Vector3.one;
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}