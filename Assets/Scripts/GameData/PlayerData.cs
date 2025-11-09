using System;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace GameData
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;
        [SerializeField] private PerkData _perks;
        [SerializeField] private StatsData _stats;
        public List<string> _storedGo = new List<string>();
        public StatsData Stats => _stats;

        public PerkData Perks => _perks;

        public IntStoredPersistantProperty Health;

        public InventoryData Inventory => _inventory;


        public PlayerData CloneData()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}