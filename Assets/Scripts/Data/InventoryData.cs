using System;
using System.Collections.Generic;
using System.Linq;
using Creatures.Definitions;
using Definitions;
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
            if (amount <= 0) return;
            var currentItem = GetItem(itemName);
            var itemDef = DefsFacade.Instance.Inventory.GetItem(itemName);
            if (itemDef.IsEmpty) return;
            if (itemDef.IsStackable)
            {
                if (currentItem != null) currentItem.count += amount;
                else if (_items.Count < DefsFacade.Instance.Player.InventoryMaxCount)
                    _items.Add(new InventoryItemData(itemName, amount));
            }
            else
            {
                for (int i = 0; i < amount; i++)
                {
                    if (_items.Count < DefsFacade.Instance.Player.InventoryMaxCount)
                        _items.Add(new InventoryItemData(itemName, 1));
                }
            }
        }

        public void RemoveItem(string itemName, int amount)
        {
            if (amount <= 0) return;

            var currentItem = GetItem(itemName);
            if (currentItem == null) return;
            var itemDef = DefsFacade.Instance.Inventory.GetItem(itemName);
            if (itemDef.IsEmpty) return;
            if (itemDef.IsStackable)
            {
                currentItem.count -= amount;
                if (currentItem.count <= 0)
                {
                    _items.Remove(currentItem);
                }
            }
            else
            {
                _items.Remove(currentItem);

                for (int i = 0; i < amount - 1; i++)
                {
                    currentItem = GetItem(itemName);
                    if (currentItem != null) _items.Remove(currentItem);
                }
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