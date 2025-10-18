using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Creatures.Definitions
{
    [Serializable]
    public struct PlayerDefs
    {
        public int InventoryMaxCount;
        public int MaxHealth;
    }
}