using System;
using System.Globalization;
using Definitions;
using GameData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Stats
{
    public class StatItem : MonoBehaviour, IItemRenderer<StatDef>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private TextMeshProUGUI _currentValue;
        [SerializeField] private TextMeshProUGUI _increaseValue;
        [SerializeField] private Image _progress;
        [SerializeField] private Image _selector;

        private GameSession _session;
        private StatsType _statType;

        private void Start()
        {
            _session = FindFirstObjectByType<GameSession>();
        }

        public void SetItem(StatDef curDef, int id)
        {
            _session ??= FindFirstObjectByType<GameSession>();
            _statType = curDef.StatType;
            _icon.sprite = curDef.Icon;
            _label.text = Localization.LocalizationManager.Instance.GetLocalizeText(curDef.Name);
            var currentValue = _session.StatsModel.GetLevel(curDef.StatType).Value;
            _currentValue.text = currentValue.ToString(CultureInfo.InvariantCulture);
            _selector.gameObject.SetActive(curDef.StatType == _session.StatsModel.SelectedStats.Value);
            var nextLevel = _session.StatsModel.GetLevel(curDef.StatType ,_session.StatsModel.NumberLevel(_statType)+1);
            if (nextLevel.Price == null)
            {
                _progress.fillAmount = 1;
                _increaseValue.gameObject.SetActive(false);
            }
            else
            {
                _increaseValue.gameObject.SetActive(true);
                _increaseValue.text =$"+{(nextLevel.Value - currentValue).ToString(CultureInfo.InvariantCulture)}";
                var currentLevel = _session.StatsModel.NumberLevel(_statType);
                var allLevels = DefsFacade.Instance.StatsRepository.GetItem(_statType.ToString()).Levels.Length;
                _progress.fillAmount = (float)currentLevel / (allLevels-1);
                
            }
        }

        public void SeletStat()
        {
            _session.StatsModel.SelectedStats.Value = _statType;
        }
    }
}