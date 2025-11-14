using System;
using System.Collections;
using GameData;
using Unity.Cinemachine;
using UnityEngine;

namespace CameraSet
{
    public class ShakeCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineBasicMultiChannelPerlin _shake;
        [SerializeField] private float _shakeIntensity;
        [SerializeField] private float _shakeTime;
        private Coroutine _shakeCoroutine;
        private GameSession _gameSesion;
        

        public void Shake()
        {
            if (_shakeCoroutine != null) StopCoroutine(_shakeCoroutine);
            _shakeCoroutine =  StartCoroutine(ShakeCoroutine());
            
        }
        public IEnumerator ShakeCoroutine()
        {
            _shake.FrequencyGain = _shakeIntensity;
            yield return new WaitForSeconds(_shakeTime);
            _shake.FrequencyGain = 0;
            _shakeCoroutine = null;
        }
    }
}