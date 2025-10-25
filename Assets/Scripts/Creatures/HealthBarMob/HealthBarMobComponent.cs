using System;
using Subscribe;
using Subscribe.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Creatures.HealthBarMob
{
    public class HealthBarMobComponent : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private CreatureHealthComponent _creatureHealth;
        private ComposideDisposible _trash = new ComposideDisposible();

        private void Start()
        {
            _creatureHealth = _creatureHealth ?? GetComponentInParent<CreatureHealthComponent>();
            _trash.Retain(_creatureHealth.OnDamage.SubsctibeDisposable(OnDamage));
            _trash.Retain(_creatureHealth.OnDeath.SubsctibeDisposable(OnDeath));
            OnDamage();
        }

        private void OnDeath()
        {
            _trash.Dispose();
            Destroy(gameObject);
        }

        private void OnDamage()
        {
            var relativeHealth = (float)_creatureHealth.currentHealth / _creatureHealth.MaxHealth;
            _healthBar.fillAmount = relativeHealth;
        }
    }
}