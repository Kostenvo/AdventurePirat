using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Scriptes.Chekers
{
    public class ChekerColider:ChekerSurfaceBase
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Collider2D _collider2D;
        private bool _isTouched;
        public override bool CheckSurface()
        {
            return _isTouched;
        }

        private void OnTriggerEnter2D(Collider2D other) 
        {
            _isTouched = _collider2D.IsTouchingLayers(_layerMask);
            Debug.Log(_isTouched);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _isTouched = _collider2D.IsTouchingLayers(_layerMask);
            Debug.Log(_isTouched);
        }
        
    }
}