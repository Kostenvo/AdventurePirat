using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class PredefinedItemController<TItem, TDataType> : ItemController<TItem, TDataType>  where TItem : MonoBehaviour , IItemRenderer<TDataType>
    {
        public PredefinedItemController(TItem itemPrefab, Transform containerItems) : base(itemPrefab, null)
        {
            _items = containerItems.GetComponentsInChildren<TItem>();
        }

        public override void Rebuild(IList<TDataType> items)
        {
            if (items.Count > _items.Count) return;
            base.Rebuild(items);
        }
    }
}