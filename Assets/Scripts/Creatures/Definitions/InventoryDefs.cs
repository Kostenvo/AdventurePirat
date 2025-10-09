using System;
using System.Linq;
using UnityEngine;

namespace Creatures.Definitions
{
    [Serializable]
    public class InventoryDefs
    {
        [SerializeField] InventoryItemDef[] _inventoryItems;

        public InventoryItemDef GetItem(string itemName) => _inventoryItems.FirstOrDefault();
        
        
#if UNITY_EDITOR
        public InventoryItemDef[] Items => _inventoryItems;
#endif
    }

    [Serializable]
    public struct InventoryItemDef
    {
        [SerializeField] private string _name;
        public string Name => _name;
        public bool IsEmpty => string.IsNullOrEmpty(Name);
    }
}