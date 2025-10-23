using System;
using System.Collections;
using Sound.Extensions;
using TMPro;
using UI.Dialoge;
using UnityEngine;

namespace UI.Dialog
{
    public class DialogBoxController : MonoBehaviour  
    {
        private static readonly int ShowKey = Animator.StringToHash("Show");
        [SerializeField] private GameObject _dialogBox;
        [SerializeField] private TextMeshProUGUI _dialogText;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _typingTime;

        [Header("Audio")]
        [SerializeField] private AudioClip _audioOpen;

        [SerializeField] private AudioClip _audioTyping;
        [SerializeField] private AudioClip _audioClose;
        private AudioSource _audioSource;
        
        private DialogData _dialogData;
        private int _dialogIndex;

        private Coroutine _dialogCoroutine;

        private void Start()
        {
           if(_audioSource == null) _audioSource = SoundExtensions.GetSfxAudioSourceSource();
            _dialogText.text = String.Empty;
        }

        public void SetDialog(DialogData dialogData)
        {
            if(_audioSource == null) _audioSource = SoundExtensions.GetSfxAudioSourceSource();
            _dialogData = dialogData;
            _dialogText.text = string.Empty;
            _dialogBox.SetActive(true);
            _animator.SetBool(ShowKey, true);
        }

   

        private void OnShowAnimation()
        {
            _dialogIndex = 0;
            _audioSource.PlayOneShot(_audioOpen);
            Typing();
        }


        public void OnSkip()
        {
            if(_dialogCoroutine == null) return;
            StopCoroutine(_dialogCoroutine);
            _dialogText.text = _dialogData.Text[_dialogIndex];
            _dialogCoroutine = null;
        }

        public void OnContinue()
        {
            if (_dialogCoroutine != null) OnSkip();
            else
            {
                _dialogIndex++;
                if (_dialogIndex < _dialogData.Text.Length)
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
            _dialogText.text = string.Empty;
           _dialogCoroutine = StartCoroutine(StartTyping());
        }

        private IEnumerator StartTyping()
        {
            var currentSentence = _dialogData.Text[_dialogIndex];
            foreach (var typingText in currentSentence)
            {
                _dialogText.text += typingText;
                _audioSource.PlayOneShot(_audioTyping);
                yield return new WaitForSeconds(_typingTime);
            }
            _dialogCoroutine = null;
        }

        private void OnClose()
        {
            _dialogBox.SetActive(false);
            _dialogText.text = string.Empty;
        }
    }
}