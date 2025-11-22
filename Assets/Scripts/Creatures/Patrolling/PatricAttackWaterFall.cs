using Creatures.Boss;
using UnityEngine;

namespace Creatures.Patrolling
{
    public class PatricAttackWaterFall : StateMachineBehaviour
    {
        private WaterFall _waterFall;
        override public void OnStateEnter(Animator patric, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _waterFall ??= patric.GetComponent<WaterFall>();
            _waterFall.WaterFallAnimation();
        }
    }
}