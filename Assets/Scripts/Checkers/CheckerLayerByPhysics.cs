using Scripts.Checkers;
using UnityEditor;
using UnityEngine;

namespace Checkers
{
    public class CheckerLayerByPhysics:CheckerSurfaceBase 
    {
        [SerializeField] float _radius;
        [SerializeField] LayerMask _layerMask;
        
        public override bool CheckSurface()
        {
           bool isGround = Physics2D.OverlapCircle(transform.position, _radius,_layerMask);
           return isGround;
        }

        private void OnDrawGizmosSelected()
        {
            Handles.color = CheckSurface() ? CheckerColor.Green : CheckerColor.Red;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
    }
}