using System;
using Data;
using Definitions;
using Subscribe;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

namespace GameData
{
    public class StatsModel
    {
        private PersistantProperty<StatsType> _selectedStats;
        public PersistantProperty<StatsType> SelectedStats => _selectedStats;

        private PlayerData _data;
        private Action _perkChange;


        public ActionDisposable Subscribe(Action perkChange)
        {
            _perkChange += perkChange;
            return new ActionDisposable(() => _perkChange -= perkChange);
        }

        public StatsModel(PlayerData data)
        {
            _data = data;
            _selectedStats = new PersistantProperty<StatsType>();
            _selectedStats.Value = StatsType.Health;
        }

        public bool UpgradeStats(StatsType stats)
        {
            var nextStats = GetLevel(stats, NumberLevel(stats) + 1);
            if (nextStats.Price == null) return false;
            if (!_data.Inventory.IsEnoughItem(nextStats.Price)) return false;
            _data.Inventory.RemoveItem(nextStats.Price.name, nextStats.Price.count);
            _data.Stats.UpgradeStat(stats);
            _perkChange?.Invoke();
            return true;
        }

        public StatLevel GetNextLevel(StatsType stats) => GetLevel(stats, NumberLevel(stats) + 1);

        public StatLevel GetLevel(StatsType stats, int level = -1)
        {
            var def = GetStatDef(stats);
            if (level >= def.Levels.Length) return default;
            var currentLevel = level == -1 ? NumberLevel(stats) : level;
            return def.Levels[currentLevel];
        }

        public int NumberLevel(StatsType stats) => _data.Stats.GetLevel(stats);

        private StatDef GetStatDef(StatsType stats) => DefsFacade.Instance.StatsRepository.GetItem(stats.ToString());
    }
}