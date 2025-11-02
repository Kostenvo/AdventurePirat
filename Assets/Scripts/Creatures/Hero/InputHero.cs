using Checkers;
using Creatures;
using Creatures.Hero;
using Definitions;
using GameData;
using PlayerInput;
using Subscribe;
using Subscribe.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace Scripts.Creatures.Hero
{
    [RequireComponent(typeof(MoveBase))]
    public class InputHero : MonoBehaviour
    {
        [SerializeField] private HeroMove _move;
        [SerializeField] private CheckInteractableObject _interaction;
        [SerializeField] private HeroAttackObject _attack;
        [SerializeField] private HeroHealthComponent _health;
        [SerializeField] private HeroInventory _inventory;
        private InputSystem_Actions _actions;
        
        private ComposideDisposible trash = new ComposideDisposible();
        private GameSession _gameSession;

        private void Start()
        {
            _gameSession = FindAnyObjectByType<GameSession>();
        }
        private void Awake()
        {
            _actions = new InputSystem_Actions();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _move ??= GetComponent<HeroMove>();
            _interaction ??= GetComponentInChildren<CheckInteractableObject>();
            _attack ??= GetComponentInChildren<HeroAttackObject>();
            _health ??= GetComponentInChildren<HeroHealthComponent>();
        }
#endif

        private void OnEnable()
        {
            _actions.Enable();
            trash.SubscribeInputPreformed(_actions.Player.Move, OnMovePerformedHandler);
            trash.SubscribeInputCanceled(_actions.Player.Move, OnMoveCanceledHandler);
            trash.SubscribeInputPreformed(_actions.Player.Interact, OnInteractPerformedHandler);
            trash.SubscribeInputStarted(_actions.Player.Attack, OnAttackStartHandler);
            trash.SubscribeInputStarted(_actions.Player.Throw, OnTrowStartecHandler);
            trash.SubscribeInputCanceled(_actions.Player.Throw, OnThrowCanceledHandler);
            trash.SubscribeInputPreformed(_actions.Player.NextItem, OnNextItemPerformedHandler);
        }

        private void OnDisable()
        {
            _actions.Disable();
            trash.Dispose();
        }

        private void OnTrowStartecHandler(InputAction.CallbackContext obj) => _attack.StartButtonThrow();

        private void OnThrowCanceledHandler(InputAction.CallbackContext obj)
        {
            var def = _gameSession.QuickInventory.GetCurrentItemDef();
            if (def.HasType(InventoryItemType.Throwable)) _attack.EndButtonThrow();
            else if (def.HasType(InventoryItemType.Healable)) _health.HeroHeal();
            else if (def.HasType(InventoryItemType.Speadable)) _move.SpeedUpPosion();

        }

        private void OnNextItemPerformedHandler(InputAction.CallbackContext obj) => _inventory.NextInInventory();


        private void OnAttackStartHandler(InputAction.CallbackContext obj) => _attack.Attack();

        private void OnInteractPerformedHandler(InputAction.CallbackContext obj) => _interaction.Interact();

        private void OnMovePerformedHandler(InputAction.CallbackContext context)
        {
            var moveDirection = context.ReadValue<Vector2>();
            _move.SetDirection(moveDirection);
        }

        private void OnMoveCanceledHandler(InputAction.CallbackContext context)
        {
            var moveDirection = context.ReadValue<Vector2>();
            _move.SetDirection(moveDirection);
        }
    }
}