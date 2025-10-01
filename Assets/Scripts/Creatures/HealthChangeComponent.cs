using Scripts.Creatures;
using UnityEngine;

namespace Creatures
{
    public class HealthChangeComponent : MonoBehaviour
    {
        [SerializeField] private int _healthChangeAmount;
        public void ChangeHealth(GameObject objectWithHealth)
        {
            objectWithHealth.GetComponent<IHealthChangeComponent>()?.ChangeHealth(_healthChangeAmount);
        }
    }
}