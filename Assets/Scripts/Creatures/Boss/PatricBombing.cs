using UnityEngine;

namespace Creatures.Boss
{
    public class PatricBombing : StateMachineBehaviour
    {
        private BombingComponent _bombingComponent;
        override public void OnStateEnter(Animator patric, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _bombingComponent ??= patric.GetComponent<BombingComponent>();
            _bombingComponent.Spawn();
        }
    }
}