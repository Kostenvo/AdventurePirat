using System;
using System.Collections.Generic;
using System.Linq;
using Creatures.Definitions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public class InventoryData
    {
        [SerializeField] private List<InventoryItemData> _items;

        public void AddItem(string itemName, int amount)
        {
            var curentItem = GetItem(itemName);
            var item = DefsFacade.Instance.Inventory.GetItem(itemName);
            if(item.IsEmpty) return;
            if (curentItem == null)
            {
                _items.Add(new InventoryItemData(itemName, amount));
            }
            else
            {
                curentItem.count += amount;
            }
        }

        public void RemoveItem(string itemName, int amount)
        {
            var curentItem = GetItem(itemName);
            if (curentItem == null) return;
            var item = DefsFacade.Instance.Inventory.GetItem(itemName);
            if(item.IsEmpty) return;
            curentItem.count -= amount;
            if (curentItem.count <= 0)
            {
                _items.Remove(curentItem);
            }
        }

        private InventoryItemData GetItem(string itemName) => _items.FirstOrDefault(x => x.name.Contains(itemName));

        public int CountItem(string itemName)
        {
            var curentItem = GetItem(itemName);
            if (curentItem == null) return 0;
            else return curentItem.count;
        }
    }

    [Serializable]
    public class InventoryItemData
    {
        [InventoryId] public string name;
        public int count;

        public InventoryItemData(string name, int count)
        {
            this.name = name;
            this.count = count;
        }
    }
}