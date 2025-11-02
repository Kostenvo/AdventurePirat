using System;
using Creatures.Definitions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Definitions
{
    [Serializable]
    public class PotionsDef : DefRepository<PotionDef>
    {
        
    }

    [Serializable]
    public struct PotionDef : IHaveId
    {
        [InventoryId] [SerializeField] private string _name;
        [SerializeField] private float _effectValue;
        [SerializeField] private float _cooldownValue;


        public float EffectValue => _effectValue;

        public float CooldownValue => _cooldownValue;

        public string Name => _name;
    }
    
}