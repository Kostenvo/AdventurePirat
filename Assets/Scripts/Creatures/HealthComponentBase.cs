using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Creatures
{
    public class HealthComponentBase : MonoBehaviour, IHealthChangeComponent
    {
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDeath;
        public int currentHealth;

        protected virtual int _currentHealth
        {
            set { currentHealth = value; }
            get { return currentHealth; }
        }

        public void ChangeHealth(int amount)
        {
            if (amount > 0)
                Heal(amount);
            else
                Damage(-amount);
        }

        private void Damage(int amount)
        {
            if (_currentHealth <= 0) return;
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                _onDeath?.Invoke();
            }
            else
            {
                _onDamage?.Invoke();
            }
        }

        private void Heal(int amount)
        {
            _currentHealth += amount;
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }

            _onHeal?.Invoke();
        }
    }
}