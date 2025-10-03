using System.Collections;
using UnityEngine;

namespace Creatures.Patrolling
{
    public class PointPatrol: Patrol
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private MoveBase _creature;
        private float _distanceToPoint = 0.1f;
        public int _currentPoint = 0;
        
        public override IEnumerator DoPatrol()
        {
            while (true)
            {
                var distance = Vector3.Distance(_points[_currentPoint].position, transform.position);
                if (distance > _distanceToPoint)
                {
                    _creature.SetDirection(PointDistance(_points[_currentPoint].position));
                }
                else
                {
                    _currentPoint = (int)Mathf.Repeat(++_currentPoint, _points.Length);
                    
                }
                yield return null;
            }
        }
        
        private Vector2 PointDistance(Vector3 target)
        {
            var directionX = (target - transform.position).normalized.x;
            // return new Vector2(Mathf.Sign(directionX), 0f);
            return new Vector2(directionX, 0f);
        }

    }
}