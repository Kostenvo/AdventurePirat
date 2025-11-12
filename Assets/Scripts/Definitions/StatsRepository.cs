using System;
using Data;
using UnityEngine;

namespace Definitions
{
    [Serializable]
    public class StatsRepository : DefRepository<StatDef>
    {
        
    }
    [Serializable]
    public struct StatDef : IHaveId
    {
        [SerializeField] private string _name;
        [SerializeField] private StatsType _statType;
        [SerializeField] private Sprite _icon;
        [SerializeField] private StatLevel[] _levels;


        public Sprite Icon => _icon;

        public StatLevel[] Levels => _levels;

        public StatsType StatType => _statType;
        public string Name => _name;
    }

    [Serializable]
    public struct StatLevel
    {
        [SerializeField] private float _value;
        [SerializeField] private InventoryItemData _price;

        public InventoryItemData Price => _price;

        public float Value => _value;
    }

    public enum StatsType
    {
        Health,
        Damage,
        RangeDamage,
        CriticalDamage,
        Speed,
    }
}