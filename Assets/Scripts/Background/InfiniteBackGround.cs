using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Background
{
    public class InfiniteBackGround : MonoBehaviour
    {
        [FormerlySerializedAs("_background")] [SerializeField]
        private Transform _container;

        [SerializeField] private UnityEngine.Camera _camera;
        private Bounds _baseBounds;
        private Bounds _allBounds;

        public Vector3 _boundsToTransformDelta;
        public Vector3 _containerDelta;
        private Vector3 _screenSize;

        private void Start()
        {
            var sprites = _container.GetComponentsInChildren<SpriteRenderer>();
            if (sprites.Length == 0) return;
            _baseBounds = sprites[0].bounds;
            for (int i = 1; i < sprites.Length; i++)
                _baseBounds.Encapsulate(sprites[i].bounds);
            _allBounds = _baseBounds;
            _boundsToTransformDelta = transform.position - _allBounds.center;
            _containerDelta = _container.position - _allBounds.center;
        }

        private void LateUpdate()
        {
            var min = _camera.ViewportToWorldPoint(Vector3.zero);
            var max = _camera.ViewportToWorldPoint(Vector3.one);
            _screenSize = new Vector3(max.x - min.x, max.y - min.y);
            
            _allBounds.center = transform.position - _boundsToTransformDelta;
            var camPosition = _camera.transform.position.x;
            var screenLeft = new Vector3(camPosition - _screenSize.x /2, _baseBounds.center.y);
            var screenRight = new Vector3(camPosition + _screenSize.x /2, _baseBounds.center.y);
            if (!_allBounds.Contains(screenLeft))
            {
                InstantiateContainer(_allBounds.min.x - _baseBounds.extents.x);
            }

            if (!_allBounds.Contains(screenRight))
            {
                InstantiateContainer(_allBounds.max.x + _baseBounds.extents.x);
            }
        }

        private void InstantiateContainer(float boundCenterX)
        {
            var newBounds = new Bounds(new Vector3(boundCenterX, _baseBounds.center.y), _baseBounds.size);
            _allBounds.Encapsulate(newBounds);

            _boundsToTransformDelta = transform.position - _allBounds.center;
            var newContainerXPos = boundCenterX + _containerDelta.x;
            var newPosition = new Vector3(newContainerXPos, _container.position.y);

            Instantiate(_container, newPosition, Quaternion.identity, transform);
        }
        
        private void OnDrawGizmosSelected()
        {
            _allBounds.DrowBound();
        }
    }
}