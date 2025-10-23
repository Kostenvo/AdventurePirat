using System;
using UI.Dialoge;
using UnityEngine;

namespace UI.Dialog
{
    public class ShowDialogComponent : MonoBehaviour
    {
        [SerializeField] private DialogType _dialogType;
        [SerializeField] private DialogData _dialogData;
        [SerializeField] private DialogDef _dialogDef;
        private DialogBoxController _dialogBoxController;

        private DialogData CurrentDialogData
        {
            get
            {
                switch (_dialogType)
                {
                    case DialogType.Dialog:
                        return _dialogData;
                    case DialogType.Def:
                        return _dialogDef._dialogData;
                    default: throw new NullReferenceException();
                }
            }
        }


        public void ShowDialog()
        {
            if (_dialogBoxController == null) _dialogBoxController = FindAnyObjectByType<DialogBoxController>();
            _dialogBoxController.SetDialog(CurrentDialogData);
        }

        public void ShowDialogDef(DialogDef dialogDef)
        {
            _dialogDef = dialogDef;
            _dialogType = DialogType.Def;
            ShowDialog();
        }
    }

    public enum DialogType
    {
        Def,
        Dialog,
    }
}