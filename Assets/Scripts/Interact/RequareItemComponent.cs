using Data;
using GameData;
using UnityEngine;
using UnityEngine.Events;

namespace Interact
{
    public class RequireItemComponent : MonoBehaviour
    {
        [SerializeField] private InventoryItemData[] _requiredItems;
        [SerializeField] private UnityEvent _enoughEvent;
        [SerializeField] private UnityEvent _notEnoughEvent;
        [SerializeField] private bool _removeRequiredItems;

        public void CheckRequiredItems()
        {
          
            if (GameSession.Instance.PlayerData.Inventory.IsEnoughItem(_requiredItems))
            {
                RemuveInInventory();
                _enoughEvent?.Invoke();
            }
            else
            { 
                _notEnoughEvent?.Invoke();
            }
        }

        private void RemuveInInventory()
        {
            foreach (var item in _requiredItems)
                GameSession.Instance.PlayerData.Inventory.RemoveItem(item.name, item.count);
        }
    }
}