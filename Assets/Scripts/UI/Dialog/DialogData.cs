using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sound.Extensions
{
    [Serializable]
    public struct DialogData
    {
        [SerializeField] private DialogType _dialogType;
        [SerializeField] private Sentence[] _sentence;

        public DialogType DialogType => _dialogType;
        public Sentence[] Sentence => _sentence;
    }

    [Serializable]
    public struct Sentence
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _text;
        [SerializeField] private DialogPosition _side;

        public DialogPosition Side => _side;

        public Sprite Icon => _icon;

        public string Text => _text;
    }

    public enum DialogPosition
    {
        Left,
        Right
    }

    public enum DialogType
    {
        Personalized,
        Single
    }
}