using Data;
using Definitions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PriceItem : MonoBehaviour
    {
        [SerializeField] private Image PriceIcon;
        [SerializeField] private TextMeshProUGUI PriceText;

        public void SetPrice(InventoryItemData price)
        {
            PriceIcon.sprite = DefsFacade.Instance.Inventory.GetItem(price.name).Image;
            PriceText.text = price.count.ToString();
        }
        
    }
    
}