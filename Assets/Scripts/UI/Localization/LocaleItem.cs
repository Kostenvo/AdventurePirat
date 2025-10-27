using System;
using Subscribe;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Localization
{
    [Serializable]
    public class LocaleItem : MonoBehaviour, IItemRenderer<LocaleData>
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _selector;
        private LocaleData _data;
        ComposideDisposible _trash = new ComposideDisposible();

        public void SetItem(LocaleData list, int id)
        {
            _data = list;
            _text.text = list.locale;
            _trash.Retain(LocalizationManager.Instance.CurrentLocale.SubscribeAndInvoke(SetSelector));
        }

        private void SetSelector(string newvalue, string oldvalue)
        {
            _selector.gameObject.SetActive(_data.locale.Contains(newvalue));
        }

        public void ChangeLocale()
        {
            LocalizationManager.Instance.CurrentLocale.Value = _data.locale;
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }

    [Serializable]
    public struct LocaleData
    {
        public string locale;
    }
}