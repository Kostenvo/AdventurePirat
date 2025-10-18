using Checkers;
using Creatures;
using Creatures.Hero;
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
        [SerializeField] private MoveBase _move;
        [SerializeField] private CheckInteractableObject _interaction;
        [SerializeField] private HeroAttackObject _attack;
        [SerializeField] private HeroHealthComponent _health;
        private InputSystem_Actions _actions;
        
        private ComposideDisposible trash = new ComposideDisposible();

        private void Awake()
        {
            _actions = new InputSystem_Actions();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _move ??= GetComponent<MoveBase>();
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
            trash.SubscribeInputPreformed(_actions.Player.Heal, OnHealPerformedHandler);
        }

        private void OnDisable()
        {
            _actions.Disable();
            trash.Dispose();
        }

        private void OnHealPerformedHandler(InputAction.CallbackContext obj) => _health.HeroHeal();

        private void OnTrowStartecHandler(InputAction.CallbackContext obj) => _attack.StartButtonThrow();

        private void OnThrowCanceledHandler(InputAction.CallbackContext obj) => _attack.EndButtonThrow();


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