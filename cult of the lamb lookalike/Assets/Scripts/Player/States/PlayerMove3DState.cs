using Player.States.Attacks;
using StateMachine;
using UnityEngine;

namespace Player.States
{
    [CreateAssetMenu(menuName = "States/Player/Move 3D")]
    public class PlayerMove3DState : State<PlayerStateMachine>
    {
        [SerializeField, Range(250f, 500f)] private float _speed = 300f;

        private Vector3 _playerInput;

        public override void Enter(PlayerStateMachine parent)
        {
            base.Enter(parent);
            parent.Animations.PlayMove();
        }

        public override void Tick(float deltaTime)
        {
            _playerInput = new Vector3(_runner.Movement.x, 0, _runner.Movement.y).normalized;
        }

        public override void FixedTick(float fixedDeltaTime)
        {
            _runner.Move(_playerInput * (_speed * fixedDeltaTime));
        }

        public override void ChangeState()
        {
            if(_runner.AttackPressed)
            {
                _runner.SetState(typeof(PlayerAttackState));
                return;
            }

            if(_runner.RollPressed)
            {
                _runner.SetState(typeof(PlayerRollState));
                return;
            }
            if (_playerInput == Vector3.zero)
            {
                _runner.SetState(typeof(PlayerIdleState));
            }
        }

        
    }
}

