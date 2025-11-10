using System;
using System.Collections.Generic;
using System.Linq;
using Definitions;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace GameData
{
    [Serializable]
    public class StatsData
    {
        [SerializeField] private List<Stat> _stats = new List<Stat>();

        public void UpgradeStat(StatsType stat)
        {
            var def = DefsFacade.Instance.StatsRepository.GetItem(stat.ToString());
            var level = GetLevel(stat);
            if (level == 0) _stats.Add(new Stat(stat, 1));
            else if (level < def.Levels.Length) GetStat(stat)._level ++;
        }

        public int GetLevel(StatsType stat)
        {
            var def = GetStat(stat);
            return def?._level ?? 0;
        }
        
        private Stat GetStat(StatsType stat) => _stats.FirstOrDefault(x => x.StatT == stat);

        [Serializable]
        public class Stat
        {
            [SerializeField] private StatsType _stat;
            [SerializeField] public int _level;

            public Stat(StatsType stat, int level)
            {
                _stat = stat;
                _level = level;
            }

            public StatsType StatT => _stat;
        }
    }
}