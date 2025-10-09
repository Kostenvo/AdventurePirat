using UnityEngine;
using UnityEngine.Serialization;

namespace Creatures.Definitions
{
    [CreateAssetMenu(menuName = "Definitions/Facade", fileName = "Facade")]
    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private InventoryDefs _inventory;

        public InventoryDefs Inventory => _inventory;

        private static DefsFacade _facade;
        public static DefsFacade Instance => _facade == null ? GetDefsFacade() : _facade;

        private static DefsFacade GetDefsFacade()
        {
            return _facade = Resources.Load<DefsFacade>("Facade");
        }
    }
}