using System;
using Sound.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace UI.Dialog
{
    public class ShowDialogComponent : MonoBehaviour
    {
        [SerializeField] private SentenceType _sentenceType;
        [SerializeField] private DialogData _dialogData;
        [SerializeField] private DialogDef _dialogDef;
        [SerializeField] private UnityEvent _onFinishedDialog;
        private DialogBoxController _dialogBoxController;

        private DialogData CurrentDialogData
        {
            get
            {
                switch (_sentenceType)
                {
                    case SentenceType.Dialog:
                        return _dialogData;
                    case SentenceType.Def:
                        return _dialogDef._dialogData;
                    default: throw new NullReferenceException();
                }
            }
        }


        public void ShowDialog()
        {
            GameObject boxController;
            switch (_dialogData.DialogType)
            {
                case DialogType.Personalized:
                    boxController = GameObject.FindGameObjectWithTag("PersonalizedDialogBox");
                    break;
                case DialogType.Single:
                    boxController = GameObject.FindGameObjectWithTag("DialogBox");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _dialogBoxController = boxController.GetComponent<DialogBoxController>();
            _dialogBoxController.SetDialog(CurrentDialogData , _onFinishedDialog);
        }

        public void ShowDialogDef(DialogDef dialogDef)
        {
            _dialogDef = dialogDef;
            _sentenceType = SentenceType.Def;
            ShowDialog();
        }
    }

    public enum SentenceType
    {
        Def,
        Dialog,
    }
}