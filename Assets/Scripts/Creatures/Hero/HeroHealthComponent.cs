using System;
using GameData;

namespace Scripts.Creatures.Hero
{
    public class HeroHealthComponent : HealthComponentBase
    {
        private GameSession _gameSession;

        protected override int _currentHealth
        {
            get => currentHealth = _gameSession.PlayerData.Health;
            set => currentHealth = _gameSession.PlayerData.Health = value;
        }

        private void Start()
        {
            _gameSession = FindAnyObjectByType<GameSession>();
            _currentHealth = _gameSession.PlayerData.Health;
        }
        
    }
}