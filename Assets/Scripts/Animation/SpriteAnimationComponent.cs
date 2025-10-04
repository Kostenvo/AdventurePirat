using System;
using UnityEngine;
using UnityEngine.Events;

namespace Animation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimationComponent : MonoBehaviour
    {
        [SerializeField] private bool _loop = true;

        [SerializeField] [Range(0, 60)] private int _framePerSecond = 10;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private AnimationClips[] _animationClips;
        [SerializeField] private string _animationClipName;
        private AnimationClips _currentAnimationClip;
        private float _frameRate;
        private float _nextFrameTime;
        private int _frameIndex;

        private void Start()
        {
            if (!string.IsNullOrEmpty(_animationClipName))
                ApplyClip(_animationClipName);
            if (_animationClips != null && _animationClips.Length > 0) 
                _currentAnimationClip = _animationClips[0];
            _spriteRenderer = _spriteRenderer ? _spriteRenderer : GetComponent<SpriteRenderer>();
            RestartAnim();
        }

        public void ApplyClip(string clipName)
        {
            if (_animationClips == null || _animationClips.Length == 0)
            {
                Debug.LogError($"[{gameObject.name}] No animation clips available! Please assign animation clips in the Inspector.", this);
                return;
            }
            var isApply = false;
            foreach (var clip in _animationClips)
            {
                if (clip.ClipName.Contains(clipName))
                {
                    _currentAnimationClip = clip;
                    _frameIndex = 0;
                    isApply = true;
                    enabled = true;
                    break;
                }
            }
            if (!isApply)
            {
                Debug.LogWarning($"[{gameObject.name}] Animation clip '{clipName}' not found! Available clips: {string.Join(", ", System.Array.ConvertAll(_animationClips, clip => clip.ClipName))}", this);
            }
        }

        private void Update() => Animated();
        private void OnEnable() => RestartAnim();

        public void SetLoopAnimation(bool value) => _loop = value;

        private void Animated()
        {
            if (Time.time < _nextFrameTime) return;
            if (_currentAnimationClip.Sprites == null || _currentAnimationClip.Sprites.Length == 0) return;
            if (_frameIndex >= _currentAnimationClip.Sprites.Length)
            {
                if (_loop)
                    _frameIndex = 0;
                else
                {
                    enabled = false;
                    _currentAnimationClip.OnAnimationFinished?.Invoke();
                    return;
                }
            }

            _nextFrameTime = Time.time + _frameRate;
            _spriteRenderer.sprite = _currentAnimationClip.Sprites[_frameIndex];
            _frameIndex++;
        }

        private void RestartAnim()
        {
            _frameIndex = 0;
            _frameRate = 1.0f / _framePerSecond;
            _nextFrameTime = Time.time;
        }

        [Serializable]
        public struct AnimationClips
        {
            public string ClipName;
            public Sprite[] Sprites;
            public UnityEvent OnAnimationFinished;
        }
    }
}