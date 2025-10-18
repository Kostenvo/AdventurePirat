using UnityEngine;

namespace Sound.Extensions
{
    public static class SoundExtensions
    {
        private static AudioSource _sfXaudioSource;
        private static string _sfxTeg = "SFXSource";

        public static AudioSource GetSfxAudioSourceSource()
        {
            // Проверяем, что объект существует и не был уничтожен
            if (_sfXaudioSource == null || _sfXaudioSource.gameObject == null)
            {
                _sfXaudioSource = GameObject.FindWithTag(_sfxTeg)?.GetComponent<AudioSource>();
            }
            return _sfXaudioSource;
        }

    }
}