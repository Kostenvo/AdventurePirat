
using EditorTools;
using PlayerInput;
using Scripts.Checkers;
using Scripts.Interact;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace Scripts.Creatures.Hero
{
    [RequireComponent(typeof(MoveBase))]
    public class InputHero : MonoBehaviour
    {
        [Interface(typeof(IMovable))]
        [SerializeField] private Object _movableObject;
        [SerializeField] private  MoveBase _move;
        [SerializeField] private CheckInteractableObject _interaction;
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
        }

        private void OnDisable()
        {
            _actions.Disable();
            _actions.Player.Move.performed -= OnMovePerformedHandler;
            _actions.Player.Move.canceled -= OnMoveCanceledHandler;
            _actions.Player.Interact.performed -= OnInteractPerformedHandler;
        }

        private void OnInteractPerformedHandler(InputAction.CallbackContext obj)
        {
            _interaction.Check();
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