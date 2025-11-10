using System.Collections.Generic;
using Data;
using Subscribe;
using UnityEngine;

namespace UI.Localization
{
    public class LocalizationManager
    {
        public static readonly LocalizationManager Instance;
        public StoredStringPersistentProperty CurrentLocale = new StoredStringPersistentProperty();
        private Dictionary<string, string> _localeDictionary = new Dictionary<string, string>();
        private ComposideDisposible _trash = new ComposideDisposible();

        static LocalizationManager() => Instance = new LocalizationManager();

        public LocalizationManager()
        {
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
				if (string.IsNullOrEmpty(locale._key)) 
					continue;
				
				// Use upsert to avoid KeyAlreadyExists when there are duplicate keys in the file
				_localeDictionary[locale._key] = locale._value ?? string.Empty;
            }
        }

        public string GetLocalizeText(string key) =>
            _localeDictionary.TryGetValue(key, out string value) ? value : $"###{key}###";
        
        
    }
}