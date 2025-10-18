using Definitions;
using Sound;
using UnityEngine;

namespace UI
{
    public class SoundMenuWindow : AnimatedWindow
    {
        [SerializeField] private AudioSettingsWidget _sfxSettingsWidget;
        [SerializeField] private AudioSettingsWidget _musicSettingsWidget;

        protected override void Start()
        {
            _sfxSettingsWidget.SetVolume(GameSettingsFacade.Instance.AudioSettings.SfxVolume);
            _musicSettingsWidget.SetVolume(GameSettingsFacade.Instance.AudioSettings.MusicVolume);
            base.Start();
        }
        
    }
}