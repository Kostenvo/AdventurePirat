using System;
using Scripts.Creatures.Hero;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Camera
{
    [RequireComponent(typeof(CinemachineCamera))]
    public class CameraSetTarget : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _targetCamera;
        private InputHero _hero;

        private void Start()
        {
            _targetCamera = _targetCamera?? GetComponent<CinemachineCamera>();
            _hero = FindAnyObjectByType<InputHero>();
            _targetCamera.Follow = _hero.transform;
        }
    }
}