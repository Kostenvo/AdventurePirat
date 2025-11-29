using System;
using System.Collections.Generic;
using Data;
using GameData;
using Subscribe;
using UnityEngine;

namespace UI
{
    public class QuickInventoryController : MonoBehaviour
    {
        [SerializeField] private QIItem _itemPrefab;
        [SerializeField] private Transform _containerItems;

        private List<QIItem> _items = new List<QIItem>();
        private ItemController<QIItem , InventoryItemData> _itemController;
        private ComposideDisposible _trash = new ComposideDisposible();

        private void Start()
        {
            _itemController = new ItemController<QIItem , InventoryItemData>(_itemPrefab, _containerItems); 
            _trash.Retain( GameSession.Instance.QuickInventory.Subscribe(Rebuild));
            Rebuild("",0);
        }

        private void Rebuild(string name, int value)
        {
            var inventory = GameSession.Instance.QuickInventory.QuickInventory;
            _itemController.Rebuild(inventory);
        }

        private void OnDestroy() => _trash.Dispose();
    }
}