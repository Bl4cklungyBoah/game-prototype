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

        [SerializeField] private Animator _animator;
        public PlayerAnimations Animations { get; private set; }

        //this Vector2 can be used to determine changes on states
        public Vector2 Movement { get; private set; }

        //the sprite starts facing right so this is true
        public bool IsFacingRight => _isFacingRight;
        private bool _isFacingRight = true;

        public bool RollPressed => _rollPressed;
        private bool _rollPressed;

        public bool AttackPressed => _attackPressed;
        private bool _attackPressed;

        protected override void Awake()
        {
            base.Awake();
            Animations = new PlayerAnimations(_animator);
        }

        private void OnEnable()
        {
            _playerInput.MovementEvent += HandleMove;
            _playerInput.RollEvent += HandleRoll;
            _playerInput.RollCancelledEvent += HandleCancelledRoll;
            _playerInput.AttackEvent += HandleAttack;
        }

        private void OnDisable()
        {
            _playerInput.MovementEvent -= HandleMove;
            _playerInput.RollEvent -= HandleRoll;
            _playerInput.RollCancelledEvent -= HandleCancelledRoll;
            _playerInput.AttackEvent -= HandleAttack;
        }

        private void HandleMove(Vector2 movement)
        {
            Movement = movement;
            CheckFlipSprite(movement);
        }

        private void HandleRoll()
        {
            _rollPressed = true;
        }

        private void HandleCancelledRoll()
        {
            _rollPressed = false;
        }

        private void HandleAttack(bool isPressed)
        {
            Debug.Log("attack button has been pressed");
            _attackPressed = isPressed;
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

