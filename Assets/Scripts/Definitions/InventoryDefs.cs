using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
        public Sprite Image;
        public InventoryItemType[] ItemTypes;
        public string Name => _name;
        public bool IsEmpty => string.IsNullOrEmpty(Name);
        public bool HasType(InventoryItemType type) => ItemTypes.Contains(type); 
    }

    public enum InventoryItemType
    {
        Stackable,
        Throwable,
        Usable,
    }
}