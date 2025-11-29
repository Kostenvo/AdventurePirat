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
        private PerkDef _perk;




        public void SetItem(PerkDef perk, int id)
        {
            _perk = perk;
            _iconSprite.sprite = perk.IconImage;
            _lock.SetActive(!GameSession.Instance.PerksModel.CanUsePerk(perk.Name));
            _select.SetActive(GameSession.Instance.PerksModel.SelectionPerk.Value.Contains(perk.Name));
            _use.SetActive(GameSession.Instance.PerksModel.IsActivePerk(perk.Name));
        }

        public void onClick()
        {
            GameSession.Instance.PerksModel.SelectionPerk.Value = _perk.Name;
        }
    }
}