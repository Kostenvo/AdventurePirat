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
        [SerializeField] private string _activePerk;
        [SerializeField] private List<string> UnlockedPerks = new List<string>();


        public string ActivePerk => _activePerk;

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
                _activePerk = perk;
            }
        }

        public bool IsUnlockedPerk(string perkName)
        {
            return UnlockedPerks.Contains(perkName);
        }
    }
}