using System;

namespace GameData
{
    [Serializable]
    public class PlayerData
    {
        public int Health;
        public int Coins;
        public bool IsArmed;

        public PlayerData CloneData()
        {
            return (PlayerData)MemberwiseClone();
        }
    }
}