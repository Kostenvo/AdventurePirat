using PlayerInput;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scriptes.Creatures.Hero
{
    [RequireComponent(typeof(IMovable))]
    public class InputHero : MonoBehaviour
    {
        private InputSystem_Actions _actions;
        private IMovable _move;

        private void Awake()
        {
            _actions = new InputSystem_Actions();
            _move = GetComponent<IMovable>();
        }

        private void OnDisable()
        {
            _actions.Disable();
            _actions.Player.Move.started -= OnMoveStartedHandler;
            _actions.Player.Move.performed -= OnMovePerformedHandler;
            _actions.Player.Move.canceled -= OnMoveCanceledHandler;
        }

        private void OnEnable()
        {
            _actions.Enable();
            _actions.Player.Move.started += OnMoveStartedHandler;
            _actions.Player.Move.performed += OnMovePerformedHandler;
            _actions.Player.Move.canceled += OnMoveCanceledHandler;
        }

        private void OnMoveStartedHandler(InputAction.CallbackContext context)
        {
           // move = context.ReadValue<Vector2>();
           // var moveDirection = context.ReadValue<Vector2>();
           // _move.SetDirection(moveDirection);
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