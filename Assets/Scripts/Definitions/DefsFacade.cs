using System;
using Creatures.Definitions;
using Unity.VisualScripting;
using UnityEngine;

namespace Definitions
{
    [CreateAssetMenu(menuName = "Definitions/Facade", fileName = "Facade")]
    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private InventoryDefs _inventory;
        [SerializeField] private ThrowableItemsDef _throwableItem;
        [SerializeField] private PotionsDef _potion;
        [SerializeField] private PlayerDefs _player;
        
        public ThrowableItemsDef ThrowableItem => _throwableItem;
        public InventoryDefs Inventory => _inventory;
        public PotionsDef Potion => _potion;

        public PlayerDefs Player => _player;


        private static DefsFacade _facade;
        public static DefsFacade Instance => _facade == null ? GetDefsFacade() : _facade;

        private static DefsFacade GetDefsFacade()
        {
            return _facade = Resources.Load<DefsFacade>("Facade");
        }
    }
}