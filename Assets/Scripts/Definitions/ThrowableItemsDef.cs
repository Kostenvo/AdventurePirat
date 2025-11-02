using System;
using System.Linq;
using Creatures.Definitions;
using UnityEngine;

namespace Definitions
{
    [Serializable]
    public class ThrowableItemsDef : DefRepository<ThrowableItem>
    {

    }

    [Serializable]
    public struct ThrowableItem:IHaveId
    {
    [InventoryId] [SerializeField] private string name;
    public GameObject TrowItem;
    public string Name => name;
    }
}