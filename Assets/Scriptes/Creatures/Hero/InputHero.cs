using System;
using PlayerInput;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scriptes.Creatures.Hero
{
    [RequireComponent(typeof(MoveBase))]
    public class InputHero : MonoBehaviour
    {
        [SerializeField] private  MoveBase _move;
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
        }

        private void OnDisable()
        {
            _actions.Disable();
            _actions.Player.Move.performed -= OnMovePerformedHandler;
            _actions.Player.Move.canceled -= OnMoveCanceledHandler;
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