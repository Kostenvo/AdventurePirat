using Particles;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PatricLevelUp : StateMachineBehaviour
{
 
    private CircularSpawner _circleSpawner;

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator patric, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _circleSpawner ??= patric.GetComponent<CircularSpawner>();
        _circleSpawner.LevelUp();
    }

   
}
