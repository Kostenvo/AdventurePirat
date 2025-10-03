using Checkers;
using Scripts.Creatures;
using UnityEditor;
using UnityEngine;

namespace Creatures
{
    public class CheckAttackObjectBase : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMaskForAttack;
        [SerializeField] private int _damage;
        [SerializeField] private float _radius;
        private Collider2D[] _colliders;

        public virtual void Attack()
        {
            _colliders = Physics2D.OverlapCircleAll(transform.position, _radius, _layerMaskForAttack);
            foreach (var collide in _colliders)
            {
                collide.GetComponent<HealthComponentBase>()?.ChangeHealth(-_damage);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Handles.color = CheckerColor.Red;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
    }
}