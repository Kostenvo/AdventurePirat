

using System;
using UnityEngine;

namespace UI.Button
{
    public class MyButtonMenu: UnityEngine.UI.Button
    {
        [SerializeField] private GameObject _normalText;
        [SerializeField] private GameObject _pressedText;
        
        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);
            _pressedText?.SetActive(state == SelectionState.Pressed);
            _normalText?.SetActive(state != SelectionState.Pressed);
        }
    }
}