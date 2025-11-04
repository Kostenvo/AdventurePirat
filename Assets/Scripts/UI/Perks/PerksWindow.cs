using System;
using System.Linq;
using Data;
using Definitions;
using GameData;
using Subscribe;
using Subscribe.Extensions;
using TMPro;
using UnityEngine;

namespace UI.Perks
{
    public class PerksWindow : AnimatedWindow
    {
        [SerializeField] private PriceItem _priceItem;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private PerkItem _perkItem;
        [SerializeField] private Transform _perksContainer;
        [SerializeField] private UnityEngine.UI.Button _useButton;
        [SerializeField] private UnityEngine.UI.Button _buyButton;

        private StringPersistantProperty _selectedPerk =>_session.PerksModel.SelectionPerk;
        private PredefinedItemController<PerkItem, PerkDef> _perks;
        private GameSession _session;
        private ComposideDisposible _trash = new ComposideDisposible();
        private float _timeScale;

        protected override void Start()
        {
            _timeScale = Time.timeScale;
            Time.timeScale = 0;
            _perks = new PredefinedItemController<PerkItem, PerkDef>(_perkItem, _perksContainer);
            _session = FindFirstObjectByType<GameSession>();
            base.Start();
            _selectedPerk.Value = DefsFacade.Instance.Perks.Items[0].Name;
            _trash.Retain(_selectedPerk.Subscribe((value, oldValue) => Redraw()));
            _trash.Retain(_session.PerksModel.Subscribe(Redraw));
            Redraw();
        }

        public void OnBuy()
        {
            _session.PerksModel.BuyPerk(_selectedPerk.Value);
        }

        public void OnUse()
        {
            _session.PerksModel.UsePerk(_selectedPerk.Value);
        }

        private void Redraw()
        {
            var perkDef = DefsFacade.Instance.Perks.GetItem(_selectedPerk.Value);
            _priceItem.SetPrice(perkDef.Price);
            _descriptionText.text = perkDef.Description;
            _buyButton.gameObject.SetActive(!_session.PerksModel.CanUsePerk(_selectedPerk.Value));
            _useButton.gameObject.SetActive(_session.PerksModel.CanUsePerk(_selectedPerk.Value));
            _buyButton.interactable = _session.PerksModel.CanBuyPerk(_selectedPerk.Value);
            _useButton.interactable = !_session.PerksModel.IsActivePerk(_selectedPerk.Value);
            _perks.Rebuild(DefsFacade.Instance.Perks.Items);
        }
        protected override void OnClosedAnimation()
        {
            Time.timeScale = _timeScale;
            base.OnClosedAnimation();
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}