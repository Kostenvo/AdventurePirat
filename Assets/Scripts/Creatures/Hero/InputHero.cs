
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
        [SerializeField] private  MoveBase _move;
        [SerializeField] private CheckInteractableObject _interaction;
        [SerializeField] private HeroAttackObject _attack;
        private InputSystem_Actions _actions;

        private void Awake()
        {
            _actions = new InputSystem_Actions();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_move == null)  
                _move = GetComponent<MoveBase>();
        }
#endif

        private void OnEnable()
        {
            _actions.Enable();
            _actions.Player.Move.performed += OnMovePerformedHandler;
            _actions.Player.Move.canceled += OnMoveCanceledHandler;
            _actions.Player.Interact.performed += OnInteractPerformedHandler;
            _actions.Player.Attack.started += OnAttackStartHandler;
            _actions.Player.Throw.performed += OnThrowPerformesHandler;
        }

        private void OnDisable()
        {
            _actions.Disable();
            _actions.Player.Move.performed -= OnMovePerformedHandler;
            _actions.Player.Move.canceled -= OnMoveCanceledHandler;
            _actions.Player.Interact.performed -= OnInteractPerformedHandler;
            _actions.Player.Attack.started -= OnAttackStartHandler;
            _actions.Player.Throw.performed -= OnThrowPerformesHandler;

        }

        private void OnThrowPerformesHandler(InputAction.CallbackContext obj)
        {
            _attack.Throw();
        }

        private void OnAttackStartHandler(InputAction.CallbackContext obj)
        {
            _attack.Attack();
        }

        private void OnInteractPerformedHandler(InputAction.CallbackContext obj)
        {
            _interaction.Interact();
        }

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