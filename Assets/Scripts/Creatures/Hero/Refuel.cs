using System;
using GameData;
using UnityEngine;

namespace Creatures.Hero
{
    public class Refuel : MonoBehaviour
    {
        private GameSession _gameSession;
        private void Start()
        {
            _gameSession ??= FindAnyObjectByType<GameSession>();
        }

        public void RefuelBy(float amount)
        {
            _gameSession.PlayerData.Fuel = amount;
        }
    }
}