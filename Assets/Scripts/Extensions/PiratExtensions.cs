using UnityEngine;

namespace Extensions
{
    public static class PiratExtensions
    {
        public static bool IsInLayer(this GameObject gameObject, LayerMask layer)
        {
            return layer == (layer | (1 << gameObject.layer));
        }
    }
}