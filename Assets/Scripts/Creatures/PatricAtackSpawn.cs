using Particles;
using UnityEngine;

public class PatricAtackSpawn : StateMachineBehaviour
{
    private CircularSpawner _circleSpawner;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator patric, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _circleSpawner ??= patric.GetComponent<CircularSpawner>();
        _circleSpawner.Spawn();
    }

   
}
