using Checkers;
using Creatures.Hero;
using UnityEngine;

namespace Scripts.Creatures.Hero
{
    public class ArmHeroComponent : MonoBehaviour
    {
        public void ArmHero(GameObject hero)
        {
            hero.GetComponentInChildren<HeroAttackObject>()?.ArmHero();
        }
    }
}