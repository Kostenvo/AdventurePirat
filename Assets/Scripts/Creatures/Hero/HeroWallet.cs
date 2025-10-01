using Scripts.GameData;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Creatures.Hero
{
    public class HeroWallet : MonoBehaviour, IChangeCoinInWallet
    {
        [SerializeField] private UnityEvent addedCoinEvent;
        [SerializeField] private UnityEvent removedCoinEvent;
        [SerializeField] ParticleSystem coinParticle;
        public int coin;
        private GameSession _gameSession;

        private int Coin
        {
            get => coin = _gameSession.PlayerData.Coins;
            set => coin = _gameSession.PlayerData.Coins = value;
        }

        private void Start()
        {
            _gameSession = FindAnyObjectByType<GameSession>();
            coin  = _gameSession.PlayerData.Coins;
        }


        public void CoinChange(int coinToChange)
        {
            if (coinToChange < 0)
            {
                RemoveCoin(coinToChange);
            }
            else
            {
                AddCoin(coinToChange);
            }
        }

        private void RemoveCoin(int coinToRemove)
        {
            if (Coin <= 0) return;
            int maxCoinsForRemove = Mathf.Min(-coinToRemove, Coin);
            Coin -= maxCoinsForRemove;
            SpawnParticles(maxCoinsForRemove);
        }

        private void SpawnParticles(int maxCoinsForRemove)
        {
            var burst = coinParticle.emission.GetBurst(0);
            burst.count = maxCoinsForRemove;
            coinParticle.emission.SetBurst(0, burst);
            coinParticle.Play();
        }

        private void AddCoin(int coinToAdd)
        {
            Coin += coinToAdd;
            addedCoinEvent.Invoke();
        }

    }
}