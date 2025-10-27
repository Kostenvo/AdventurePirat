using System;
using Subscribe;
using TMPro;
using UnityEngine;

namespace UI.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizeText : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private TextMeshProUGUI _text;
        ComposideDisposible _trash = new ComposideDisposible();

        private void Start()
        {
            _text  = _text?? GetComponent<TextMeshProUGUI>();
           _trash.Retain( LocalizationManager.Instance.CurrentLocale.SubscribeAndInvoke(ChangeLocale));
        }

        private void ChangeLocale(string newvalue, string oldvalue)
        {
            _text.text = LocalizationManager.Instance.GetLocalizeText(_key);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}