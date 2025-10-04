using System;
using UnityEngine;

namespace TimeComponent
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float _cooldownTime;
        private float _nextCooldownTime;

        public void ResetCooldown() => _nextCooldownTime = Time.time + _cooldownTime;
        public bool IsReady() => Time.time >= _nextCooldownTime;
    }
}