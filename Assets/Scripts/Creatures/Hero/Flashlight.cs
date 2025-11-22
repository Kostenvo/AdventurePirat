using System;
using GameData;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Creatures.Hero
{
    public class Flashlight : MonoBehaviour
    {
        [SerializeField] private Light2D _flashlight;
        [SerializeField] private float _fuelPerSecond;
        private PlayerData _playerData;
        private float _baseIntensity;
        private void Start()
        {
            _playerData = FindAnyObjectByType<GameSession>().PlayerData;
            _baseIntensity = _flashlight.intensity;
        }

        public void ActivateFlashlight()
        {
            var takeOff = gameObject.activeSelf;
            gameObject.SetActive(!takeOff);
        }

        private void Update()
        {
            var fuelPerSecond = _fuelPerSecond * Time.deltaTime;
            _playerData.Fuel = Mathf.Clamp(_playerData.Fuel - fuelPerSecond, 0, 100);
            var flashlightIntensity = Mathf.Clamp(_playerData.Fuel/20, 0, 1);
            _flashlight.intensity = flashlightIntensity * _baseIntensity;
            if (flashlightIntensity <= 0) gameObject.SetActive(false);
        }
    }
}