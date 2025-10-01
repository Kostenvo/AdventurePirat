using UnityEngine;

namespace Checkers
{
    public class CheckLayerByTrigger:CheckerSurfaceBase
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Collider2D _collider2D;
        public bool _isTouched;
        public override bool CheckSurface() => _isTouched;

        private void OnTriggerEnter2D(Collider2D other) => _isTouched = _collider2D.IsTouchingLayers(_layerMask);

        private void OnTriggerExit2D(Collider2D other) => _isTouched = _collider2D.IsTouchingLayers(_layerMask);
    }
}