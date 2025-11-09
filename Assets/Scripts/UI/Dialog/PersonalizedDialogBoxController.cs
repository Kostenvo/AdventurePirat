using System;
using Sound.Extensions;
using UnityEngine;

namespace UI.Dialog
{
    public class PersonalizedDialogBoxController : DialogBoxController
    {
        [SerializeField] public DialogContainer _dialogRight;

        protected override DialogContainer CurrentDialog =>
            _dialogData.Sentence[_dialogIndex].Side == DialogPosition.Left ? _dialog : _dialogRight;
        

        protected override void ShowDialogBox()
        {
            _dialogRight.gameObject.SetActive(_dialogData.Sentence[_dialogIndex].Side == DialogPosition.Right);
            _dialog.gameObject.SetActive(_dialogData.Sentence[_dialogIndex].Side == DialogPosition.Left);
            base.ShowDialogBox();
        }

        protected override void OnClose()
        {
            _dialogRight.gameObject.SetActive(false);
            _dialog.gameObject.SetActive(false);
            base.OnClose();
        }
    }
}