using System;
using Definitions;
using GameData;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Perks
{
    public class PerkItem : MonoBehaviour, IItemRenderer<PerkDef>
    {
        [SerializeField] private Image _iconSprite;
        [SerializeField] private GameObject _lock;
        [SerializeField] private GameObject _select;
        [SerializeField] private GameObject _use;
        private GameSession _gameSession;
        private PerkDef _perk;


        private void Start()
        {
            _gameSession = FindAnyObjectByType<GameSession>();
        }

        public void SetItem(PerkDef perk, int id)
        {
            _perk = perk;
            _gameSession ??= _gameSession = FindAnyObjectByType<GameSession>();
            _iconSprite.sprite = perk.IconImage;
            _lock.SetActive(!_gameSession.PerksModel.CanUsePerk(perk.Name));
            _select.SetActive(_gameSession.PerksModel.SelectionPerk.Value.Contains(perk.Name));
            _use.SetActive(_gameSession.PerksModel.IsActivePerk(perk.Name));
        }

        public void onClick()
        {
            _gameSession.PerksModel.SelectionPerk.Value = _perk.Name;
        }
    }
}