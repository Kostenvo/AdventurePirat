using Scripts.Checkers;
using UnityEngine;

namespace Scripts.Creatures.Hero
{
    public class ArmHeroComponent : MonoBehaviour
    {
        public void ArmHero(GameObject hero)
        {
            hero.GetComponentInChildren<CheckAttackObject>()?.ArmHero();
        }
    }
}