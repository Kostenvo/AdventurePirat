using System;
using Data;
using Definitions;
using UnityEngine;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class VolumeBinding : MonoBehaviour
    {
        [SerializeField] protected SoundType _soundType;
        [SerializeField] protected AudioSource _source;
        private StoredFloatPersistentProperty _volume;

        private void Start()
        {
            _source ??= GetComponent<AudioSource>();
            SetVolumeType();
            if (_volume == null) return;
            _volume.ValueChanged += SetVolume;
            SetVolume(_volume.Value,0);
        }

        private void SetVolume(float newvalue, float oldvalue)
        {
            _source.volume = newvalue;
        }

        private void SetVolumeType()
        {
            switch (_soundType)
            {
                case SoundType.Music:
                    _volume = GameSettingsFacade.Instance.AudioSettings.MusicVolume;
                    break;
                case SoundType.SFX:
                    _volume = GameSettingsFacade.Instance.AudioSettings.SfxVolume;
                    break;
                default:
                    Debug.LogError($"Sound type {_soundType} is not supported");
                    break;
            }
        }

        private void OnDestroy()
        {
            _volume.ValueChanged -= SetVolume;
        }
    }
}