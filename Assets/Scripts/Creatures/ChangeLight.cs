using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Creatures
{
    public class ChangeLight : MonoBehaviour
    {
        [SerializeField] private Light2D[] _lights;
        [ColorUsage(true, true)]
        [SerializeField] private Color _lightColor;

        public void ChangeLightColor()
        {
            foreach (var light in _lights)
            {
                light.color = _lightColor;
            }
        }
    }
}