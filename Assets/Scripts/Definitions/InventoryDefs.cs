using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Creatures.Definitions
{
    [Serializable]
    public struct InventoryDefs
    {
        [SerializeField] InventoryItemDef[] _inventoryItems;

        public InventoryItemDef GetItem(string itemName) => _inventoryItems.FirstOrDefault(x => x.Name.Contains(itemName));
        
        
#if UNITY_EDITOR
        public InventoryItemDef[] Items => _inventoryItems;
#endif
    }

    [Serializable]
    public struct InventoryItemDef
    {
        [SerializeField] private string _name;
        [FormerlySerializedAs("_isStackable")] public bool IsStackable;
        public string Name => _name;
        public bool IsEmpty => string.IsNullOrEmpty(Name);
    }
}