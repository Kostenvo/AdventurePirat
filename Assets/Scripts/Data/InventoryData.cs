using System;
using System.Collections.Generic;
using System.Linq;
using Creatures.Definitions;
using Definitions;
using Subscribe;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class InventoryData
    {
        [SerializeField] private List<InventoryItemData> _items;

        public Action<string, int> ChangeInventory;

        public InventoryItemData[] Items => _items.ToArray();

        public IDisposable Subscribe(Action<string, int> action)
        {
            ChangeInventory += action;
            return new ActionDisposable(() => ChangeInventory -= action);
        }

        public bool IsEnoughItem(params InventoryItemData[] prices)
        {
            var dictionaryPrice = new Dictionary<string, int>();
            foreach (var price in prices)
            {
                if (!dictionaryPrice.ContainsKey(price.name)) dictionaryPrice.Add(price.name, price.count);

                else dictionaryPrice[price.name] += price.count;
            }

            foreach (var price in dictionaryPrice)
            {
                if (price.Value > CountItem(price.Key)) return false;
            }

            return true;
        }


        public void AddItem(string itemName, int amount)
        {
            if (amount <= 0) return;
            var currentItem = GetItem(itemName);
            var itemDef = DefsFacade.Instance.Inventory.GetItem(itemName);
            if (itemDef.IsEmpty) return;
            if (itemDef.HasType(InventoryItemType.Stackable))
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

            ChangeInventory?.Invoke(itemName, amount);
        }

        public void RemovePriceItem(params InventoryItemData[] prices)
        {
            foreach (var price in prices)
            {
                RemoveItem(price.name, price.count);
            }
        }

        public void RemoveItem(string itemName, int amount)
        {
            if (amount <= 0) return;
            var currentItem = GetItem(itemName);
            if (currentItem == null) return;
            var itemDef = DefsFacade.Instance.Inventory.GetItem(itemName);
            if (itemDef.IsEmpty) return;
            if (itemDef.HasType(InventoryItemType.Stackable))
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

            ChangeInventory?.Invoke(itemName, amount);
        }

        private InventoryItemData GetItem(string itemName) => _items.FirstOrDefault(x => x.name.Contains(itemName));

        public InventoryItemData[] GetQuickInventory(params InventoryItemType[] types)
        {
            var quickInventory = new List<InventoryItemData>();
            foreach (var item in _items)
            {
                var itemDef = DefsFacade.Instance.Inventory.GetItem(item.name);
                if (types.All(x => itemDef.HasType(x)))
                {
                    quickInventory.Add(item);
                }
            }

            return quickInventory.ToArray();
        }

        public int CountItem(string itemName)
        {
            var count = 0;
            foreach (var item in _items)
            {
                if (item.name.Contains(itemName)) count += item.count;
            }

            return count;
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