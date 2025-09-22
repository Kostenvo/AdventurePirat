using System;
using UnityEditor;
using UnityEngine;

namespace Scriptes.Chekers
{
    public class ChekerPhysics:ChekerSurfaceBase 
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
            Handles.color = CheckSurface() ? ChekerColor.Green : ChekerColor.Red;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
    }
}