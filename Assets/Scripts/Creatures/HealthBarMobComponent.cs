using Subscribe;
using Subscribe.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Creatures
{
    public class HealthBarMobComponent : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private CreatureHealthComponent _creatureHealth;
        [SerializeField] private bool _destroyOnDeath = true;
        private ComposideDisposible _trash = new ComposideDisposible();

        private void Start()
        {
            _creatureHealth ??= GetComponentInParent<CreatureHealthComponent>();
            _trash.Retain(_creatureHealth.OnDamage.SubsctibeDisposable(OnDamage));
            _trash.Retain(_creatureHealth.OnDeath.SubsctibeDisposable(OnDeath));
            OnDamage();
        }

        private void OnDeath()
        {
            _trash.Dispose();
            OnDamage();
            if (_destroyOnDeath)
                Destroy(gameObject);
        }

        private void OnDamage()
        {
            var relativeHealth = (float)_creatureHealth.currentHealth / _creatureHealth.MaxHealth;
            _healthBar.fillAmount = relativeHealth;
        }
    }
}