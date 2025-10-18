using System;
using Data;
using UnityEngine;

namespace Definitions
{
    [Serializable]
    public class AudioSettingsDef
    {
        [SerializeField] private StoredFloatPersistentProperty _musicVolume;
        [SerializeField] private StoredFloatPersistentProperty _sfxVolume;
        [SerializeField] private float _test;

        public StoredFloatPersistentProperty MusicVolume => _musicVolume;

        public StoredFloatPersistentProperty SfxVolume => _sfxVolume;


        public void Validate()
        {
            _musicVolume.Validate();
            _sfxVolume.Validate();
        }
    }
}