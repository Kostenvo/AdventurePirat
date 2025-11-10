using TMPro;
using UnityEngine;

namespace UI.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizeText : BaseLocalize
    {
        [SerializeField] private string _key;
        [SerializeField] private TextMeshProUGUI _text;
        

        protected override void Start()
        {
            _text  = _text?? GetComponent<TextMeshProUGUI>();
            base.Start();
        }

        protected override void ChangeLocale()
        {
            _text.text = LocalizationManager.Instance.GetLocalizeText(_key);
        }

        
    }
}