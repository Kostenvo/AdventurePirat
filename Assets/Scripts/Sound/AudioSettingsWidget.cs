using System;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Sound
{
    public class AudioSettingsWidget : MonoBehaviour
    {
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private TextMeshProUGUI _volumeText;

        private StoredFloatPersistentProperty _volume;

        public void SetVolume(StoredFloatPersistentProperty volume)
        {
            _volume = volume;
            volume.ValueChanged += ChangeVolume;
            ChangeVolume(volume.Value ,0);
            _volumeSlider.onValueChanged.AddListener(SliderChanged);
        }

        private void SliderChanged(float volume)
        {
            _volume.Value = volume;
        }

        private void ChangeVolume(float newvalue, float oldvalue)
        {
            _volumeText.text = (Mathf.Round (newvalue * 100)).ToString ();
            _volumeSlider.normalizedValue = newvalue;
        }


        private void OnDestroy()
        {
            _volume.ValueChanged -= ChangeVolume;
        }
    }
}