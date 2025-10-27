using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UI.Localization
{
    [CreateAssetMenu(menuName = "Localization/LocaleDef", fileName = "LocaleDef")]
    public class LocaleDef : ScriptableObject
    {
        //eng https://docs.google.com/spreadsheets/d/e/2PACX-1vTTIOQHOPD5F8CK7hmy9ojLaXT5G3Jty9tZxR0zFlvsVCezOC6iDkhBCpZHuSBx-6N14yiG4JT-R0L0/pub?gid=0&single=true&output=tsv
        //ru https://docs.google.com/spreadsheets/d/e/2PACX-1vTZQGcJ5aIPQVxb5AogcSkZcnP2XLCEOZaDcpUQqWiOLr4CF2TCf44XNwxX4inCMimWD4SbkOk9_kil/pub?gid=384531933&single=true&output=tsv
        //es https://docs.google.com/spreadsheets/d/e/2PACX-1vTVIW63o6AIn1FXoJYs4kIs2GZKVOzdV4eCupKSHJY6jiaDQTjH-TlK-eKcCeBCzpQChtF4pF5LWZQz/pub?gid=1047503156&single=true&output=tsv
        [SerializeField] private string _url;
        [SerializeField] private List<Locale> _localeDefs = new List<Locale>();

        public List<Locale> LocaleDefs => _localeDefs;

        private UnityWebRequest _request;

        [ContextMenu("GetLocaleFormUrl")]
        public void UpdateLocale()
        {
            _localeDefs.Clear();
            _request = UnityWebRequest.Get(_url);
            _request.SendWebRequest().completed += GetLocale;
        }

        private void GetLocale(AsyncOperation operation)
        {
            if (operation.isDone)
            {
                var getLocale = _request.downloadHandler.text;
                var parseOnRaw = getLocale.Split("\n");
                ParseLocale(parseOnRaw);
            }
        }

        private void ParseLocale(string[] locales)
        {
            try
            {
                foreach (var locale in locales)
                {
                    var parseLocale = locale.Split("\t");
                    _localeDefs.Add(new Locale() { key = parseLocale[0], value = parseLocale[1] });
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e); 
            }
           
        }
    }

    [Serializable]
    public struct Locale
    {
        public string key;
        public string value;
    }
}