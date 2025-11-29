using UnityEngine;

namespace Creatures.Boss
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