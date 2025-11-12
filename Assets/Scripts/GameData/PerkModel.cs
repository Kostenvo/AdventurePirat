using System;
using Data;
using Definitions;
using Subscribe;
using TimeComponent;
using UnityEngine;

namespace GameData
{
    public class PerkModel 
    {
        private StringStoredPersistantProperty _selectionPerk;
        public StringStoredPersistantProperty SelectionPerk => _selectionPerk;

        private PlayerData _playerData;

        private Action _perkChange;

        private Cooldown _cooldownPerk = new Cooldown(1);

        public Cooldown CooldownPerk => _cooldownPerk;

        public string ActivePerk => _playerData.Perks.ActivePerk.Value;

        public ActionDisposable SubscribeToActivePerk(PersistantProperty<string>.OnValueChanged onValueChanged)
        {
           return _playerData.Perks.ActivePerk.Subscribe(onValueChanged) ;
        }

        public ActionDisposable Subscribe(Action perkChange)
        {
            _perkChange += perkChange;
            return new ActionDisposable(() =>_perkChange -= perkChange);
        }

        public PerkModel(PlayerData playerData)
        {
            _playerData = playerData;
            _selectionPerk = new StringStoredPersistantProperty();
            _playerData.Perks.ActivePerk.Value ??=  "";
        }


        public bool IsActivePerk(string perk) => perk == _playerData.Perks.ActivePerk.Value;

        public void BuyPerk(string perkName)
        {
            if (CanBuyPerk(perkName))
            {
                var perkDef = DefsFacade.Instance.Perks.GetItem(perkName);
                _playerData.Perks.AddPerk(perkName);
                _playerData.Inventory.RemoveItem(perkDef.Price.name, perkDef.Price.count);
                _perkChange?.Invoke();
            }
        }

        public void UsePerk(string perkName)
        {
            if (CanUsePerk(perkName))
            {
                _playerData.Perks.SetPerk(perkName);
                _perkChange?.Invoke();
            }
        }

        public bool CanUsePerk(string perkName) => _playerData.Perks.IsUnlockedPerk(perkName);


        public bool CanBuyPerk(string perkName)
        {
            var perkDef = DefsFacade.Instance.Perks.GetItem(perkName);
            return _playerData.Inventory.IsEnoughItem(perkDef.Price);
        }
        
    }
}