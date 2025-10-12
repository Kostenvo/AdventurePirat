using Checkers;
using Creatures;
using Creatures.Hero;
using PlayerInput;
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
            _actions.Player.Move.performed += OnMovePerformedHandler;
            _actions.Player.Move.canceled += OnMoveCanceledHandler;
            _actions.Player.Interact.performed += OnInteractPerformedHandler;
            _actions.Player.Attack.started += OnAttackStartHandler;
            _actions.Player.Throw.started += OnTrowStartecHandler;
            _actions.Player.Throw.canceled += OnThrowCanceledHandler;
            _actions.Player.Heal.performed += OnHealPerformedHandler;
        }

        private void OnDisable()
        {
            _actions.Disable();
            _actions.Player.Move.performed -= OnMovePerformedHandler;
            _actions.Player.Move.canceled -= OnMoveCanceledHandler;
            _actions.Player.Interact.performed -= OnInteractPerformedHandler;
            _actions.Player.Attack.started -= OnAttackStartHandler;
            _actions.Player.Throw.started -= OnTrowStartecHandler;
            _actions.Player.Throw.canceled -= OnThrowCanceledHandler;
            _actions.Player.Heal.performed -= OnHealPerformedHandler;
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