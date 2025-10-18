using System;
using UnityEngine;

namespace Definitions
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Definitions/GameSettings")]
    public class GameSettingsFacade : ScriptableObject
    {
        [SerializeField] private AudioSettingsDef _audioSettings;

        public AudioSettingsDef AudioSettings => _audioSettings;

        private static GameSettingsFacade _facade;
        public static GameSettingsFacade Instance => _facade == null ? GetDefsFacade() : _facade;

        private static GameSettingsFacade GetDefsFacade()
        {
            return _facade = Resources.Load<GameSettingsFacade>("GameSettings");
        }

        private void OnValidate()
        {
            _audioSettings.Validate();
        }
    }
}