using System;
using Creatures.Definitions;
using Data;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Definitions
{
    [Serializable]
    public class PerksDef : DefRepository<PerkDef>
    {
        
    }

    [Serializable]
    public struct PerkDef : IHaveId
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _iconImage;
        [SerializeField] private InventoryItemData _price;
        [SerializeField] private float _cooldown;

        public float Cooldown => _cooldown;

        public string Description => _description;

        public Sprite IconImage => _iconImage;

        public InventoryItemData Price => _price;

        public string Name => _name;
    }

    
}