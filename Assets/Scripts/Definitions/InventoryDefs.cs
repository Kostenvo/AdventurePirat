using System;
using System.Linq;
using UnityEngine;

namespace Definitions
{
    [Serializable]
    public class InventoryDefs : DefRepository<InventoryItemDef>
    {
        
    }

    [Serializable]
    public struct InventoryItemDef : IHaveId
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
        Healable,
        Speadable,
    }
}