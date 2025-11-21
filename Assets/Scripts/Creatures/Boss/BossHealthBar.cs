using Extensions;
using UnityEngine;

namespace Creatures.Boss
{
    public class BossHealthBar : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _alphaChangeTime;


        public void ShowHealthBar()
        {
            this.LerpFloat(0, 1, _alphaChangeTime, ChangeAlpha);
        }

        public void HideHealthBar()
        {
            this.LerpFloat(1, 0, _alphaChangeTime, ChangeAlpha);
        }

        private void ChangeAlpha(float alpha)
        {
            _canvasGroup.alpha = alpha;
        }
    }
}