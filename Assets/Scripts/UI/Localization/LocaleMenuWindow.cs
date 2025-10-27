using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Localization
{
    public class LocaleMenuWindow : AnimatedWindow
    {
        [SerializeField] private LocaleItem _localeItem;
        [SerializeField] private Transform _container;
        private ItemController<LocaleItem,LocaleData> _localeController;
        private string[] _locale = new string[] { "eng", "ru" , "es" };

        protected override void Start()
        {
            _localeController = new ItemController<LocaleItem, LocaleData>(_localeItem, _container);
            _localeController.Rebuild(LocaleToArray(_locale));
            base.Start();
        }

        private List<LocaleData> LocaleToArray(string[] locale)
        {
            List<LocaleData> list = new List<LocaleData>();
            foreach (var local in locale)
            {
                list.Add(new LocaleData(){locale = local});
            }
            return list;
        }
    }
}