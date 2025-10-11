using System;
using UnityEngine;

namespace Sound
{
    public class AudioListComponent : MonoBehaviour
    {
        [SerializeField] private AudioForPlaying[] _audioForPlaying;
        [SerializeField] private AudioSource _audioSource;
        

        public void Play(string audioName)
        {
            if (_audioForPlaying == null || _audioForPlaying.Length < 0) return;
            if (_audioSource == null) return;
            foreach (var audioForPlaying in _audioForPlaying)
            {
                if (audioForPlaying.audioName.Contains(audioName))
                {
                    _audioSource.PlayOneShot(audioForPlaying.audioClip);
                    return;
                }
            }
        }
    }
    
    
    [Serializable]
    public struct AudioForPlaying
    {
        public string audioName;
        public AudioClip audioClip;
    }
}