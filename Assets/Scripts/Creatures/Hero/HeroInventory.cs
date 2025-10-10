using System;
using Creatures.Hero;
using GameData;
using Interact;
using Particles;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class HeroInventory : MonoBehaviour, IChangeItem
{
    [SerializeField] private ParticleSystem _coinParticle;
    [SerializeField] private ProbabilityDropComponent _probabilityDropComponent;
    [SerializeField] private HeroAttackObject _heroAttackObject;
    private GameSession _gameSession;
    private int CountItem(string item) => _gameSession.PlayerData.Inventory.CountItem(item);

    private void Start()
    {
        _gameSession = FindAnyObjectByType<GameSession>();
    }

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
        _gameSession.PlayerData.Inventory.AddItem(itemName, count);
    }

    private void RemoveCoin(int count, string itemName)
    {
        if (CountItem(itemName) <= 0) return;
        int maxCoinsForRemove = Mathf.Min(-count, CountItem(itemName));
        _gameSession.PlayerData.Inventory.RemoveItem(itemName, maxCoinsForRemove);
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
}