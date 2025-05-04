using Player.Input;
using StateMachine;
using UnityEngine;

namespace Player
{
    public class PlayerStateMachine : StateMachine<PlayerStateMachine>
    {
        [SerializeField] private PlayerInput _playerInput;
        //transform to rotate around when changing directions
        [SerializeField] private Transform _spriteTransform;
        [SerializeField] private Rigidbody _rigidbody;

        //this Vector2 can be used to determine changes on states
        public Vector2 Movement { get; private set; }

        //the sprite starts facing right so this is true
        private bool _isFacingRight = true;

        public bool RollPressed;

        private void OnEnable()
        {
            _playerInput.MovementEvent += HandleMove;
            _playerInput.RollEvent += HandleRoll;
        }

        private void OnDisable()
        {
            _playerInput.MovementEvent -= HandleMove;
            _playerInput.RollEvent -= HandleRoll;
        }

        private void HandleMove(Vector2 movement)
        {
            Movement = movement;
            CheckFlipSprite(movement);
        }

        private void HandleRoll()
        {
            RollPressed = true;
        }

        private void CheckFlipSprite(Vector2 velocity)
        {
            if ((!(velocity.x > 0f) || _isFacingRight) && (!(velocity.x < 0f) || !_isFacingRight)) return;
        
            _isFacingRight = !_isFacingRight;
            _spriteTransform.Rotate(_spriteTransform.rotation.x, 180f, _spriteTransform.rotation.z);
        }

        public void Move(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
        }

    }
}

