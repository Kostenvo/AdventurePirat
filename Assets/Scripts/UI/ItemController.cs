using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ItemController<TItem, TDataType> where TItem : MonoBehaviour , IItemRenderer<TDataType>
    {
        private TItem _itemPrefab;
        private Transform _containerItems;
        private List<TItem> _items;


        public ItemController(TItem itemPrefab, Transform containerItems)
        {
            _itemPrefab = itemPrefab;
            _containerItems = containerItems;
            _items = new List<TItem>();
        }

        public void Rebuild(IList<TDataType> items)
        {
            for (int i = _items.Count; i < items.Count; i++)
            {
                var item = Object.Instantiate(_itemPrefab, _containerItems);
                _items.Add(item);
            }

            for (int i = 0; i < items.Count; i++)
            {
                _items[i].SetItem(items[i], i);
                _items[i].gameObject.SetActive(true);
            }

            for (int i = items.Count; i < _items.Count; i++)
            {
                _items[i].gameObject.SetActive(false);
            }
        }
    }

    public interface IItemRenderer<TDataType>
    {
        public void SetItem(TDataType list, int id);
    }
}