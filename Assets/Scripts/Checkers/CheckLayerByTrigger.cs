using System;
using UnityEngine;

namespace Checkers
{
    [RequireComponent(typeof(Collider2D))]
    public class CheckLayerByTrigger:CheckerSurfaceBase
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Collider2D _collider2D;
        public bool _isTouched;
        public override bool CheckSurface() => _isTouched;

        private void Awake()
        {
            
            _collider2D = _collider2D? _collider2D: GetComponent<Collider2D>();
        }


        private void OnTriggerEnter2D(Collider2D other) => _isTouched = _collider2D.IsTouchingLayers(_layerMask);

        private void OnTriggerExit2D(Collider2D other) => _isTouched = _collider2D.IsTouchingLayers(_layerMask);
    }
}