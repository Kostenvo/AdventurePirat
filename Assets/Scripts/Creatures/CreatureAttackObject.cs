using UnityEngine;

namespace Creatures
{
    public class CreatureAttackObject : CheckAttackObjectBase
    {
        [SerializeField] private Animator _animator;
        private readonly int _attackKey = Animator.StringToHash("Attack");
        public override void Attack()
        {
            _animator.SetTrigger(_attackKey);
            base.Attack();
        }

    }
}