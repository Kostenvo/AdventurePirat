using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Dialog
{
    public class DialogContainer : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _text;

        public TextMeshProUGUI Text => _text;

        public void TrySetIcon(Sprite icon)
        {
            if (_iconImage != null )
            {
                _iconImage.sprite = icon;
                _iconImage.enabled = icon? true: false;
            }
        }
    }
}