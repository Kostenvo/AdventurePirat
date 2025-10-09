using Creatures.Definitions;
using UnityEngine;

namespace Interact
{
    public class ChangeInInventory : MonoBehaviour
    {
        [InventoryId] [SerializeField] private string _itemName;
        [SerializeField] private int _count;

        public void ChangeItem(GameObject hero)
        {
            hero.GetComponent<IChangeItem>()?.ChangeItems(_itemName, _count);
        }
            
    }
}