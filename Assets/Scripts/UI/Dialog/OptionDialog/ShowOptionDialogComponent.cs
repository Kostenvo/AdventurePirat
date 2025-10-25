using UnityEngine;

namespace UI.Dialog.OptionDialog
{
    public class ShowOptionDialogComponent : MonoBehaviour
    {
        [SerializeField] private OptionDialogData _optionDialogData;
        private OptionDialogController _optionDialogController;

        public void ShowOptionDialog()
        {
            _optionDialogController = _optionDialogController?? FindFirstObjectByType<OptionDialogController>();
            _optionDialogController.SetWindow(_optionDialogData);
        }
    }
}