using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Animation
{
    public class CircularMovement : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] _objectsForCircularMovement;
        [SerializeField] private float _speed;
        [SerializeField] private float _radius;
        private float _currentTime;


        private void OnValidate()
        {
            _objectsForCircularMovement = GetComponentsInChildren<SpriteRenderer>();
            ChangePosition();
        }

        private void Start()
        {
            _objectsForCircularMovement = _objectsForCircularMovement ?? GetComponentsInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            ChangePosition();
        }

        private void ChangePosition()
        {
            var particleAngle = Mathf.PI * 2 / _objectsForCircularMovement.Length;
            var isAllAlive = false;
            for (int i = 0; i < _objectsForCircularMovement.Length; i++)
            {
                if (!_objectsForCircularMovement[i]) continue;
                var currentAngle = particleAngle * i;
                var currentPosition = new Vector2(
                    transform.position.x + Mathf.Cos(currentAngle + _currentTime * _speed) * _radius,
                    transform.position.y + Mathf.Sin(currentAngle + _currentTime * _speed) * _radius);
                _objectsForCircularMovement[i].transform.position = currentPosition;
                isAllAlive = true;
            }
            if (!isAllAlive) Destroy(gameObject);
            _currentTime += Time.deltaTime;
        }

        private void OnDrawGizmos()
        {
            Handles.DrawWireDisc(transform.position, Vector3.forward, _radius);
        }
    }
}