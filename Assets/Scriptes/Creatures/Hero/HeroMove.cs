using UnityEngine;

namespace Scriptes.Creatures.Hero
{
    public class HeroMove : MonoBehaviour, IMovable
    {
        [SerializeField] private float moveSpeed = 5f;
        private Vector2 _moveDirection;

        private void Update()
        {
            var move = new Vector3(_moveDirection.x * moveSpeed * Time.deltaTime,
                _moveDirection.y * moveSpeed * Time.deltaTime, 0);
            transform.position += move;
        }

        public void SetDirection(Vector2 dir)
        {
            _moveDirection = dir;
        }
    }
}