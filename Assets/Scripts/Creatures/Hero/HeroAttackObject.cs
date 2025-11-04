using System;
using System.Collections;
using Creatures.Definitions;
using Data;
using Definitions;
using GameData;
using GameObjects;
using Particles;
using Sound;
using TimeComponent;
using UnityEngine;
using UnityEngine.Serialization;

namespace Creatures.Hero
{
    public class HeroAttackObject : CheckAttackObjectBase
    {
        [Header("SuperThrow")]
        [SerializeField] private int _superThrowCount;
        [SerializeField] private float _superThrowDelay;
        
        [Header("Cooldowns")] [SerializeField] private Cooldown _attackCooldown;
        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private Cooldown _SuperThrowingCooldown;

        [Header("Animations")] [SerializeField]
        private Animator _animator;

        [SerializeField] private RuntimeAnimatorController _armed;
        [SerializeField] private RuntimeAnimatorController _unarmed;
        [SerializeField] private SpawnListComponent _spawner;
        [FormerlySerializedAs("_trowSpawner")] [SerializeField] private SpawnGo _throwSpawner;
        [SerializeField] private AudioListComponent _audioList;
        private readonly int _attackKey = Animator.StringToHash("Attack");
        private readonly int _throwKey = Animator.StringToHash("Throw");
        private readonly string _throwAudioKey = "Range";
        private readonly string _attackAudioKey = "Melee";
        
        private GameSession _gameSession;
        private int _swordCount => _gameSession.PlayerData.Inventory.CountItem("Sword");

        private void Start()
        {
            _gameSession = FindAnyObjectByType<GameSession>();
            ChangeState();
        }

        public void StartButtonThrow()
        {
            _SuperThrowingCooldown.ResetCooldown();
        }
        private InventoryItemData QiItem  => _gameSession.QuickInventory.GetCurrentItem();
        private InventoryItemDef DefItem => DefsFacade.Instance.Inventory.GetItem(QiItem.name);

        public bool CanThrow()
        {
            if (DefItem.HasType(InventoryItemType.Throwable))
            {
                if (QiItem.name.Contains("Sword") && QiItem.count <= 1) return false;
                if (QiItem.name.Contains("HeroPearl") && QiItem.count <= 0) return false;
                return true;
            }else return false;
        }

        public void EndButtonThrow()
        {
            if (!CanThrow()) return;
            if (!_throwCooldown.IsReady()) return;
            if (_gameSession.PerksModel.IsActivePerk("SuppreThrow") && _SuperThrowingCooldown.IsReady())
            {
                StartCoroutine(SuperTrowingCoroutine());
            }
            else
            {
                Throw();
            }
        }

        private IEnumerator SuperTrowingCoroutine()
        {
            for (int i = 0; i < _superThrowCount; i++)
            {
                if (CanThrow()) 
                {
                    Throw();
                    yield return new WaitForSeconds(_superThrowDelay);
                }
            }
        }

        public void Throw()
        {
            var throwable = DefsFacade.Instance.ThrowableItem.GetItem(QiItem.name);
            _throwSpawner.SetPrefabe(throwable.TrowItem);
            _throwSpawner.Spawn();
            _audioList.Play(_throwAudioKey);
            _gameSession.PlayerData.Inventory.RemoveItem(QiItem.name, 1);
            _animator.SetTrigger(_throwKey);
            ChangeState();
            _throwCooldown.ResetCooldown();
        }

        public override void Attack()
        {
            if (_swordCount <= 0) return;
            if (!_attackCooldown.IsReady()) return;
            _animator.SetTrigger(_attackKey);
            _audioList.Play(_attackAudioKey);
            _spawner.SpawnParticle(ParticleType.Attack);
            base.Attack();
            _attackCooldown.ResetCooldown();
        }

        public void ChangeState() => _animator.runtimeAnimatorController = _swordCount > 0 ? _armed : _unarmed;
    }
}