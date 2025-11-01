using UnityEngine;

namespace Background
{
    public static class PrintBound
    {
        public static void DrowBound(this Bounds bounds)
        {
            Debug.DrawLine(new Vector3(bounds.min.x, bounds.min.y, bounds.min.z),
                new Vector3(bounds.max.x, bounds.min.y, bounds.min.z), Color.red);
            Debug.DrawLine(new Vector3(bounds.max.x, bounds.min.y, bounds.min.z),
                new Vector3(bounds.max.x, bounds.max.y, bounds.min.z), Color.red);
            Debug.DrawLine(new Vector3(bounds.max.x, bounds.max.y, bounds.min.z),
                new Vector3(bounds.min.x, bounds.max.y, bounds.min.z), Color.red);
            Debug.DrawLine(new Vector3(bounds.min.x, bounds.max.y, bounds.min.z),
                new Vector3(bounds.min.x, bounds.min.y, bounds.min.z), Color.red);
        }
    }
}