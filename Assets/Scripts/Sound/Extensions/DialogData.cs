using System;
using UnityEngine;

namespace UI.Dialoge
{
    [Serializable]
    public struct DialogData
    {
        [SerializeField] private string[] _text;

        public string[] Text => _text;
    }
}