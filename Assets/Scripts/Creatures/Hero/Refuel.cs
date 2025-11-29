using System;
using GameData;
using UnityEngine;

namespace Creatures.Hero
{
    public class Refuel : MonoBehaviour
    {

        public void RefuelBy(float amount)
        {
            GameSession.Instance.PlayerData.Fuel = amount;
        }
    }
}