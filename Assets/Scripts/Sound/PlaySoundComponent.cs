using Sound.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sound
{
    public class PlaySoundComponent : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;
        private AudioSource _audioSource;

        public void PlaySound()
        {
            _audioSource = SoundExtensions.GetSfxAudioSourceSource();
            
            // Проверяем, что AudioSource найден и аудиоклип назначен
            if (_audioSource != null && _audioClip != null)
            {
                _audioSource.PlayOneShot(_audioClip);
            }
            else
            {
                Debug.LogWarning("PlaySoundComponent: AudioSource или AudioClip не найден!");
            }
        }
    }
}