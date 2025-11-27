using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Creatures;
using Unity.VisualScripting;
using UnityEngine;
using Cooldown = TimeComponent.Cooldown;

namespace Creatures
{
    public class TotemTowerAI : MonoBehaviour
    {
        [SerializeField] private Cooldown _cooldownForAttack;
        private List<ShootingTrapAI> _traps = new List<ShootingTrapAI>();
        private int _currentTrap;


        private void OnValidate()
        {
            InitializationTrap();
        }

        private void Start()
        {
            if (_traps == null || _traps.Count == 0)
            {
                InitializationTrap();
            }

            _traps.ForEach(x => x.GetComponent<HealthComponentBase>()._onDeath.AddListener(() => OnTrapDeath(x)));
        }

        private void OnTrapDeath(ShootingTrapAI shootingTrapAI)
        {
            _traps.Remove(shootingTrapAI);
            if (_currentTrap > 0)
            {
                _currentTrap--;
            }
        }


        private void Update()
        {
            if (_traps.Count == 0) Destroy(gameObject, 1f);
            var isTouchedHero = IsTouchedHero();

            if (isTouchedHero && _cooldownForAttack.IsReady())
            {
                _traps[_currentTrap].Attack();
                _currentTrap = (int)Mathf.Repeat(++_currentTrap, _traps.Count);
                _cooldownForAttack.ResetCooldown();
            }
        }

        private bool IsTouchedHero()
        {
            var isTouchedHero = false;
            foreach (var trap in _traps)
            {
                if (trap.IsTouchedHero)
                {
                    isTouchedHero = true;
                    break;
                }
            }
            return isTouchedHero;
        }

        private void InitializationTrap()
        {
            _traps = GetComponentsInChildren<ShootingTrapAI>().ToList();
            _traps.ForEach(x => x.enabled = false);
        }
    }
}