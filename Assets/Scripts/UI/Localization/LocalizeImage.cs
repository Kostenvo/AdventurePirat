using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Localization
{
    public class LocalizeImage : BaseLocalize
    {
        [SerializeField] private LocaleImage[] _localeImages;
        [SerializeField] private Image _image;
        protected override void ChangeLocale()
        {
            foreach (var image in _localeImages)
            {
                if (image.LocaleId == LocalizationManager.Instance.CurrentLocale.Value)
                {
                    _image.sprite = image.Sprite;
                }
            }
        }
    }

    [Serializable]
    public struct LocaleImage
    {
        [SerializeField] private string _localeId;
        [SerializeField] private Sprite _sprite;

        public Sprite Sprite => _sprite;

        public string LocaleId => _localeId;
    }
}