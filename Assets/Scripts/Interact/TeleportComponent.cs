using System.Collections;
using UnityEngine;

namespace Interact
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _destinationTarget;
        [SerializeField] private float _changeAlphaTime;
        [SerializeField] private float _teleportTime;
        private SpriteRenderer _spriteRenderer;

        public void Teleport(GameObject target)
        {
            _spriteRenderer = target.GetComponent<SpriteRenderer>();
            StartCoroutine(StartTeleport(target));
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator StartTeleport(GameObject target)
        {
            var rigidBody = target.GetComponent<Rigidbody2D>();
            rigidBody.simulated = false;
            yield return SetAlpha(0);
            yield return SetPosition(target);
            yield return SetAlpha(1);
            rigidBody.simulated = true;
        }

        private IEnumerator SetPosition(GameObject target)
        {
            float elapsedTime = 0;
            Vector3 startPosition = target.transform.position;
            while (elapsedTime < _changeAlphaTime)
            {
                elapsedTime += UnityEngine.Time.deltaTime;
                var progress = elapsedTime / _changeAlphaTime;
                var currentPosition = Vector3.Lerp(startPosition, _destinationTarget.position, progress);
                target.transform.position = currentPosition;
                yield return null;
            }
            target.transform.position = _destinationTarget.position;
        }

        private IEnumerator SetAlpha(float alpha)
        {
            if (!_spriteRenderer) yield break;
            
            var startAlpha = _spriteRenderer.color.a; 
            float elapsedTime = 0;
            
            while (elapsedTime < _changeAlphaTime)
            {
                elapsedTime += Time.deltaTime;
                var progress = elapsedTime / _changeAlphaTime;
                var currentAlpha = Mathf.Lerp(startAlpha, alpha, progress);
                ChangeAlpha(currentAlpha);
                yield return null;
            }
            ChangeAlpha(alpha);
        }

        private void ChangeAlpha(float alpha)
        {
            var color = _spriteRenderer.color;
            color.a = alpha;
            _spriteRenderer.color = color;
        }
    }
}