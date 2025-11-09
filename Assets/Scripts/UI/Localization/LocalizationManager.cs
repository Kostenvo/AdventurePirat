using System.Collections.Generic;
using Data;
using Subscribe;
using UnityEngine;

namespace UI.Localization
{
    public class LocalizationManager
    {
        public static readonly LocalizationManager Instance;
        public StoredStringPersistentProperty CurrentLocale;
        private Dictionary<string, string> _localeDictionary = new Dictionary<string, string>();
        private ComposideDisposible _trash = new ComposideDisposible();

        static LocalizationManager() => Instance = new LocalizationManager();

        public LocalizationManager()
        {
            CurrentLocale = new StoredStringPersistentProperty();
            InitLocale();
        }

        private void InitLocale()
        {
            if (string.IsNullOrEmpty(CurrentLocale.Value)) CurrentLocale.Value = "eng";
            _trash.Retain(CurrentLocale.Subscribe(ChangeLocale));
            ChangeLocale(CurrentLocale.Value, "");
        }

        private void ChangeLocale(string currentLocale, string oldvalue)
        {
            var localeDef = Resources.Load<LocaleDef>($"Locale/{currentLocale}");
            if (!localeDef) return;
            _localeDictionary.Clear();
            foreach (var locale in localeDef.LocaleDefs)
            {
                _localeDictionary.Add(locale._key, locale._value);
            }
        }

        public string GetLocalizeText(string key) =>
            _localeDictionary.TryGetValue(key, out string value) ? value : $"###{key}###";
    }
}