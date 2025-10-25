using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Dialog.OptionDialog
{
    public class OptionDialogController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private OptionWidget _optionWidget;
        [SerializeField] private Transform _widgetContainer;
        [SerializeField] private GameObject _optionsWindow;
        private ItemController<OptionWidget, Option> _optionWidgetController;

        private void Start()
        {
            _optionWidgetController = new ItemController<OptionWidget, Option>(_optionWidget, _widgetContainer);
        }

        private OptionDialogData _optionDialogData;

        public void SetWindow(OptionDialogData optionDialogData)
        {
            _optionsWindow.SetActive(true);
            _text.text = optionDialogData.OptionDialog;
            _optionWidgetController.Rebuild(optionDialogData.Option);
        }
        
        public void Close()
        {
            _optionsWindow.SetActive(false);
        }
    }

    [Serializable]
    public struct OptionDialogData
    {
        public string OptionDialog;
        public Option[] Option;
    }

    [Serializable]
    public struct Option
    {
        public string OptionText;
        public UnityEvent OptionEvent;
    }
}