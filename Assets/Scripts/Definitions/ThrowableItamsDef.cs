using System;
using System.Linq;
using Creatures.Definitions;
using UnityEngine;

namespace Definitions
{
    [Serializable]
    public struct ThrowableItamsDef
    {

        [SerializeField] ThrowableItem[] _inventoryItems;

        public ThrowableItem GetItem(string itemName) =>
            _inventoryItems.FirstOrDefault(x => x.Name.Contains(itemName));

    }
    [Serializable]
    public struct ThrowableItem
    {
        [InventoryId] public string Name;
        public GameObject TrowItem;
    }
}