using System;
using System.Collections;
using UnityEngine;

namespace Extensions
{
    public static class PiratExtensions
    {
        public static bool IsInLayer(this GameObject gameObject, LayerMask layer)
        {
            return layer == (layer | (1 << gameObject.layer));
        }

        public static Coroutine LerpFloat(this MonoBehaviour gameObject, 
            float from, float to, float changeTime, Action<float> lerpFunction)
        {
            return gameObject.StartCoroutine(LerpFloatCoroutine(gameObject,
                from, to, changeTime, lerpFunction));
        }

        public static IEnumerator LerpFloatCoroutine(this MonoBehaviour gameObject, 
            float from, float to,float changeTime, Action<float> lerpFunction)
        {
            float elapsedTime = 0;
            
            while (elapsedTime < changeTime)
            {
                elapsedTime += Time.deltaTime;
                var progress = elapsedTime / changeTime;
                var currentAlpha = Mathf.Lerp(from, to, progress);
                lerpFunction(currentAlpha);
                yield return null;
            }
            lerpFunction(to);
        }
    }


}