using System;
using System.Collections;
using Checkers;
using UnityEngine;
using UnityEngine.Events;

namespace Creatures.Patrolling
{
    public class PlatformPatrol : Patrol
    {
        [SerializeField] private CheckLayerByTrigger _checkPit;
        [SerializeField] private CheckLayerByTrigger _checkEnemy;
        [SerializeField] private UnityEventVector2D _changeDirection;
        public float _direction = 1;

        public override IEnumerator DoPatrol()
        {
            while (true)
            {
                if (!_checkPit.CheckSurface() || _checkEnemy.CheckSurface())
                {
                    _direction *= -1;
                    _changeDirection.Invoke(Vector2.zero);
                    yield return new WaitForSeconds(0.2f);
                    _changeDirection.Invoke(Vector2.left * _direction);
                    yield return new WaitForSeconds(0.1f);
                    
                }
                else
                {
                    _changeDirection.Invoke(Vector2.left * _direction);
                    yield return null;
                }
            }
        }
    }
    [Serializable]
    public class UnityEventVector2D : UnityEvent<Vector2> { }
}