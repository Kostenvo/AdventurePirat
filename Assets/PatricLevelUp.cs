using Creatures;
using Particles;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PatricLevelUp : StateMachineBehaviour
{
    [ColorUsage(true, true)]
    [SerializeField] private Color _color;
    private ChangeLight _changeLight;
    private CircularSpawner _circleSpawner;

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator patric, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _circleSpawner ??= patric.GetComponent<CircularSpawner>();
        _changeLight ??= patric.GetComponent<ChangeLight>();
        _circleSpawner.LevelUp();
        _changeLight.ChangeLightColor(_color);
    }

   
}
