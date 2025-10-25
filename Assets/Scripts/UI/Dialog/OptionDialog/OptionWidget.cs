using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Dialog.OptionDialog
{
    public class OptionWidget : MonoBehaviour, IItemRenderer<Option>
    {
        [SerializeField] private TextMeshProUGUI optionName;
        [SerializeField] private UnityEvent onOptionSelected;
        private Option onOption;
        private int _index;

        public void SetItem(Option option, int index)
        {
            _index = index;
            onOption = option;
            optionName.text = option.OptionText;
        }

        public void InvokeOption()
        {
            onOption.OptionEvent?.Invoke();
            onOptionSelected?.Invoke();
        }
    }
}