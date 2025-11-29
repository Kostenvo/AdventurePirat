using Creatures.Hero;
using GameData;
using Interact;
using Particles;
using UnityEngine;

public class HeroInventory : MonoBehaviour, IChangeItem
{
    [SerializeField] private ParticleSystem _coinParticle;
    [SerializeField] private ProbabilityDropComponent _probabilityDropComponent;
    [SerializeField] private HeroAttackObject _heroAttackObject;

    private int CountItem(string item) => GameSession.Instance.PlayerData.Inventory.CountItem(item);



    public void ChangeItems(string itemName, int count)
    {
        if (count < 0)
        {
            RemoveCoin(count, itemName);
        }
        else
        {
            AddCoin(count, itemName);
        }

        if (itemName.Contains("Sword")) _heroAttackObject.ChangeState();
    }

    private void AddCoin(int count, string itemName)
    {
        GameSession.Instance.PlayerData.Inventory.AddItem(itemName, count);
    }

    private void RemoveCoin(int count, string itemName)
    {
        if (CountItem(itemName) <= 0) return;
        int maxCoinsForRemove = Mathf.Min(-count, CountItem(itemName));
        GameSession.Instance.PlayerData.Inventory.RemoveItem(itemName, maxCoinsForRemove);
        // if (itemName.Contains("Coin")) SpawnParticles(maxCoinsForRemove);
        if (itemName.Contains("Coin")) SpawnRBParticles(maxCoinsForRemove);
    }

    private void SpawnParticles(int maxCoinsForRemove)
    {
        var burst = _coinParticle.emission.GetBurst(0);
        burst.count = maxCoinsForRemove;
        _coinParticle.emission.SetBurst(0, burst);
        _coinParticle.Play();
    }

    private void SpawnRBParticles(int maxCoinsForRemove)
    {
        _probabilityDropComponent.SetCount(maxCoinsForRemove);
        _probabilityDropComponent.Spawn();
    }

    public void NextInInventory()
    {
        GameSession.Instance.QuickInventory.NetItem();
    }
}