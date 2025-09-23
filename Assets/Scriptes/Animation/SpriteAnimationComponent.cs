using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

namespace Scriptes.Animation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimationComponent : MonoBehaviour
    {
        [SerializeField] [Range(0, 60)] private int _framePerSecond = 10;
        [SerializeField] private bool _loop = true;
        [SerializeField] private UnityEvent _onAnimationFinished;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private float _frameRate;
        private float _nextFrameTime;
        private int _frameIndex;

        private void Awake()
        {
            _spriteRenderer = _spriteRenderer ? _spriteRenderer : GetComponent<SpriteRenderer>();
            RestartAnim();
        }

        private void Update() => Animeted();
        private void OnEnable() => RestartAnim();

        public void SetLoopAnimation(bool value) => _loop = value;

        private void Animeted()
        {
            if (Time.time < _nextFrameTime) return;
            if (_frameIndex >= _sprites.Length)
            {
                if (_loop)
                    _frameIndex = 0;
                else
                {
                    enabled = false;
                    _onAnimationFinished.Invoke();
                    return;
                }

            }

            _nextFrameTime = Time.time + _frameRate;
            _spriteRenderer.sprite = _sprites[_frameIndex];
            _frameIndex++;
        }

        private void RestartAnim()
        {
            _frameIndex = 0;
            _frameRate = 1.0f / _framePerSecond;
            _nextFrameTime = Time.time;
        }
    }
}