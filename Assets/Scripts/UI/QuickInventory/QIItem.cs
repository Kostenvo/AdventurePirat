using System;
using Data;
using Definitions;
using GameData;
using Subscribe;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class QIItem : MonoBehaviour, IItemRenderer<InventoryItemData>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _count;
        [SerializeField] private Image _selector;

        private int _id;
        private IntStoredPersistantProperty _currentSelection;

        private ComposideDisposible _trash = new ComposideDisposible();

        private void Start()
        {
            var session = FindAnyObjectByType<GameSession>();
            _trash.Retain(session.QuickInventory.CurrentSelect.SubscribeAndInvoke(ChangeSelector));
        }

        public void SetItem(InventoryItemData item, int id)
        {
            _id = id;
            var def = DefsFacade.Instance.Inventory.GetItem(item.name);
            _icon.sprite = def.Image;
            _count.text = item.count.ToString();
        }


        private void ChangeSelector(int newvalue, int _)
        {
            _selector.gameObject.SetActive(newvalue == _id);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}