using System;
using System.Collections.Generic;
using Definitions;
using UnityEngine;

namespace GameData
{
    [Serializable] 
    public class StatsData
    {
        private Dictionary<StatsType, int> _stats = new Dictionary<StatsType, int>();

        public void UpgradeStat(StatsType stat)
        {
            var def = DefsFacade.Instance.StatsRepository.GetItem(stat.ToString());
            if(GetLevel(stat)== 0) _stats.Add(stat, 1);
            else if(_stats[stat] < def.Levels.Length) _stats[stat]++;
        }

        public int GetLevel(StatsType stat)
        {
            return _stats.ContainsKey(stat) ? _stats[stat] : 0;
        }
        
    }
}