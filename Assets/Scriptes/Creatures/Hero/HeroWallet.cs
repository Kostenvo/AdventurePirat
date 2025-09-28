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
            if (coin <= 0) return;
            int maxCoinsForRemove = Mathf.Min(-coinToRemove, coin);
            coin -= maxCoinsForRemove;
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
            coin += coinToAdd;
            addedCoinEvent.Invoke();
        }
    }
}