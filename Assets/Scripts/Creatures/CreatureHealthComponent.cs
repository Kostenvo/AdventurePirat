using Scripts.Creatures;
using UnityEngine;
using UnityEngine.Serialization;

namespace Creatures
{
    public class CreatureHealthComponent : HealthComponentBase
    {
        [SerializeField] private Animator _animator;
        private static readonly int Health = Animator.StringToHash("Health");
        protected override void Damage(int amount)
        {
            base.Damage(amount);
            _animator.SetInteger(Health, _currentHealth);
        }

        protected override void Heal(int amount)
        {
            base.Heal(amount);
            _animator.SetInteger(Health, _currentHealth);
        }
    }
}