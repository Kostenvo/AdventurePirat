using System;
using Data;
using UnityEngine;

namespace GameData
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;
        public IntPersistantProperty Health;

        public InventoryData Inventory => _inventory;


        public PlayerData CloneData()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}