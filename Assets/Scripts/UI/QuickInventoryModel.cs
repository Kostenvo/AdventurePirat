using System;
using Creatures.Definitions;
using Data;
using Definitions;
using GameData;
using Subscribe;
using UnityEngine;

namespace UI
{
    public class QuickInventoryModel : IDisposable
    {
        public IntPersistantProperty CurrentSelect { get; private set; }
        public InventoryItemData[] QuickInventory => _quickInventory;
        private Action<string, int> _changeQuickInventory;

        private readonly PlayerData _data;
        private readonly ComposideDisposible _trash = new ComposideDisposible();
        private InventoryItemData[] _quickInventory;

        public InventoryItemData GetCurrentItem() => _quickInventory[CurrentSelect.Value];

        public QuickInventoryModel(PlayerData data)
        {
            _data = data;
            CurrentSelect = new IntPersistantProperty();
            _trash.Retain(_data.Inventory.Subscribe(ChangeQuickItem)); 
            ChangeQuickItem("Sword", 0);
        }

        public IDisposable Subscribe(Action<string, int> change)
        {
            _changeQuickInventory += change;
            return new ActionDisposable(() => _changeQuickInventory -= change);
        }

        private void ChangeQuickItem(string name, int value)
        {
            var defItem = DefsFacade.Instance.Inventory.GetItem(name);
            if (!defItem.HasType(InventoryItemType.Usable)) return;
            _quickInventory = _data.Inventory.GetQuickInventory(InventoryItemType.Usable);
            _changeQuickInventory?.Invoke(name, value);
            CurrentSelect.Value = Mathf.Clamp(CurrentSelect.Value, 0, _quickInventory.Length - 1);
        }

        public void NetItem()
        {
            CurrentSelect.Value = (int)Mathf.Repeat(CurrentSelect.Value + 1, _quickInventory.Length);
        }

        public void Dispose()
        {
            _trash?.Dispose();
        }
    }
}