using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


namespace Player.Input
{
    public class PlayerInput : MonoBehaviour, GameControls.IPlayerActions
    {
        public event UnityAction<Vector2> MovementEvent = delegate { };
        public event UnityAction RollEvent = delegate { };
        public event UnityAction RollCancelledEvent = delegate { };
        public event UnityAction<bool> AttackEvent = delegate { };

        private GameControls _playerActions;

        private void OnEnable()
        {
            if (_playerActions == null)
            {
                _playerActions = new GameControls();
                _playerActions.Player.SetCallbacks(this);
            }

            EnableGameplayInput();
        }

        private void OnDisable()
        {
            DisableAllInput();
        }

        public void DisableAllInput()
        {
            _playerActions.Player.Disable();
        }

        public void EnableGameplayInput()
        {
            if (_playerActions.Player.enabled)
            {
                return;
            }

            _playerActions.Player.Enable();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnRoll(InputAction.CallbackContext context)
        { 
            if(context.performed)
            {
                RollEvent?.Invoke();
            }
            if(context.canceled)

            {
                RollCancelledEvent?.Invoke();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            AttackEvent?.Invoke(context.performed);
        }
    }
}

