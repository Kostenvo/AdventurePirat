using System.Collections;
using Checkers;
using UnityEngine;

namespace Creatures.Patrolling
{
    public class PlatformPatrol : Patrol
    {
        [SerializeField] private MoveBase _creatureMove;
        [SerializeField] private CheckLayerByTrigger _checkPit;
        [SerializeField] private CheckLayerByTrigger _checkEnemy;
        public float _direction = 1;

        public override IEnumerator DoPatrol()
        {
            while (true)
            {
                if (!_checkPit.CheckSurface() || _checkEnemy.CheckSurface())
                {
                    _direction *= -1;
                    _creatureMove.SetDirection(Vector2.zero);
                    yield return new WaitForSeconds(0.2f);
                    _creatureMove.SetDirection(Vector2.left * _direction);
                    yield return new WaitForSeconds(0.1f);
                    
                }
                else
                {
                    _creatureMove.SetDirection(Vector2.left * _direction);
                    yield return null;
                }
            }
        }
    }
}