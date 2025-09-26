using UnityEngine;

namespace Scripts.Creatures.Hero
{
    public abstract class MoveBase: MonoBehaviour
    {
    public abstract void SetDirection(Vector2 dir);
    }
}