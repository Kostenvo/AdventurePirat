using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameData
{
    [Serializable]
    public class PerkData
    {
        [SerializeField] private PersistantProperty<string> _activePerk = new PersistantProperty<string>();
        [SerializeField] private List<string> UnlockedPerks = new List<string>();
        
        public PersistantProperty<string> ActivePerk => _activePerk;

        public void AddPerk(string perk)
        {
            if (!UnlockedPerks.Contains(perk))
            {
                UnlockedPerks.Add(perk);
            }
        }

        public void SetPerk(string perk)
        {
            if (UnlockedPerks.Contains(perk))
            {
                _activePerk.Value = perk;
            }
        }

        public bool IsUnlockedPerk(string perkName)
        {
            return UnlockedPerks.Contains(perkName);
        }
    }
}