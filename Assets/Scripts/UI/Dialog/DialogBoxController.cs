using System;
using System.Collections;
using Sound.Extensions;
using UnityEngine;

namespace UI.Dialog
{
    public class DialogBoxController : MonoBehaviour  
    {
        private static readonly int ShowKey = Animator.StringToHash("Show");
        [SerializeField] private GameObject _dialogBox;
        [SerializeField] protected DialogContainer _dialog;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _typingTime;

        [Header("Audio")]
        [SerializeField] private AudioClip _audioOpen;

        [SerializeField] private AudioClip _audioTyping;
        [SerializeField] private AudioClip _audioClose;
        private AudioSource _audioSource;
        
        protected DialogData _dialogData;
        protected int _dialogIndex;

        private Coroutine _dialogCoroutine;
        protected virtual DialogContainer CurrentDialog => _dialog;

        public void SetDialog(DialogData dialogData)
        {
            _dialogIndex = 0;
            if(_audioSource == null) _audioSource = SoundExtensions.GetSfxAudioSourceSource();
            _dialogData = dialogData;
            CurrentDialog.Text.text = string.Empty;
            ShowDialogBox();
            _animator.SetBool(ShowKey, true);
        }

        protected virtual void ShowDialogBox()
        {
            _dialogBox.SetActive(true);
        }

        private void OnShowAnimation()
        {
            _audioSource.PlayOneShot(_audioOpen);
            Typing();
        }


        public void OnSkip()
        {
            if(_dialogCoroutine == null) return;
            StopCoroutine(_dialogCoroutine);
            CurrentDialog.Text.text = _dialogData.Sentence[_dialogIndex].Text;
            _dialogCoroutine = null;
        }

        public void OnContinue()
        {
            if (_dialogCoroutine != null) OnSkip();
            else
            {
                _dialogIndex++;
                if (_dialogIndex < _dialogData.Sentence.Length)
                {
                    Typing();
                }
                else
                {
                    _audioSource.PlayOneShot(_audioClose);
                    _animator.SetBool(ShowKey, false);
                }
            }
        }

        private void Typing()
        {
            CurrentDialog.Text.text = string.Empty;
            CurrentDialog.TrySetIcon(_dialogData.Sentence[_dialogIndex].Icon);
            ShowDialogBox();
           _dialogCoroutine = StartCoroutine(StartTyping());
        }

        private IEnumerator StartTyping()
        {
            var currentSentence = _dialogData.Sentence[_dialogIndex].Text;
            foreach (var typingText in currentSentence)
            {
                CurrentDialog.Text.text += typingText;
                _audioSource.PlayOneShot(_audioTyping);
                yield return new WaitForSeconds(_typingTime);
            }
            _dialogCoroutine = null;
        }

        protected virtual void OnClose()
        {
            _dialogBox.SetActive(false);
            // CurrentDialog.Text.text = string.Empty;
        }
    }
}