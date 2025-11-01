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
        private GameSession _gameSession;

        public void CheckRequiredItems()
        {
            if (_gameSession == null) _gameSession = FindAnyObjectByType<GameSession>();
            var isEnought = true;
            foreach (var item in _requiredItems)
            {
                if (_gameSession.PlayerData.Inventory.CountItem(item.name) < item.count)
                {
                    isEnought = false;
                }
            }
            if (isEnought)
            {
                RemuveInInventory();
                _enoughEvent?.Invoke();
            }
            else _notEnoughEvent?.Invoke();
        }

        private void RemuveInInventory()
        {
            foreach (var item in _requiredItems)
                _gameSession.PlayerData.Inventory.RemoveItem(item.name, item.count);
        }
    }
}