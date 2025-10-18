using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sound
{
    public class AudioListComponent : MonoBehaviour
    {
        [SerializeField] private AudioForPlaying[] _audioForPlaying;
        [SerializeField] private AudioSource _audioSource;
        

        public void Play(string audioName)
        {
            if (_audioForPlaying == null || _audioForPlaying.Length < 0) return;
            if (_audioSource == null) FindAudioSource();
            foreach (var audioForPlaying in _audioForPlaying)
            {
                if (audioForPlaying.audioName.Contains(audioName))
                {
                    _audioSource.PlayOneShot(audioForPlaying.audioClip);
                    return;
                }
            }
        }

        private void FindAudioSource()
        {
            _audioSource = GameObject.FindGameObjectWithTag("SFXSource").GetComponent<AudioSource>();
        }
    }
    
    
    [Serializable]
    public struct AudioForPlaying
    {
        public string audioName;
        public AudioClip audioClip;
    }
}