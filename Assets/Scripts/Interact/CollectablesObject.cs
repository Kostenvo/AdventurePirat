﻿using System.Collections.Generic;
using Data;
using GameData;
using UnityEngine;

namespace Interact
{
    public class CollectablesObject : MonoBehaviour, IChangeItem
    {
        public List<InventoryItemData> _collectables = new List<InventoryItemData>();
        public GameSession _session;
        public void ChangeItems(string itemName, int count)
        {
            _collectables.Add(new InventoryItemData(itemName, count));
        }

        public void AddInInventory()
        {
            if (_session == null) _session = FindAnyObjectByType<GameSession>();
            foreach (var item in _collectables)
            {
                _session.PlayerData.Inventory.AddItem(item.name, item.count);
            }
            _collectables.Clear();
        }
    }
}