using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Creatures.Definitions.Editor
{
    [CustomPropertyDrawer(typeof(InventoryIdAttribute))]
    public class InventoryItemAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var inventoryDef = DefsFacade.Instance.Inventory.Items;
            var itemsNames = new List<string>();
            foreach (var item in inventoryDef)
            {
                itemsNames.Add(item.Name);
            }
            var selectedIndex = itemsNames.IndexOf(property.stringValue);
            selectedIndex =Mathf.Max(0, EditorGUI.Popup(position, property.displayName, selectedIndex,itemsNames.ToArray()));
            property.stringValue = itemsNames[selectedIndex];
        }
    }
}