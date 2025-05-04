using StateMachine;
using UnityEngine;

namespace Player.States.Attacks
{
    [CreateAssetMenu(menuName = "States/Player/Attack")]
    public class PlayerAttackState : State<PlayerStateMachine>
    {
        [SerializeField] private AttackSO _attackData;

        public override void Enter(PlayerStateMachine parent)
        {
            Debug.Log("entered attack state");
            base.Enter(parent);
            parent.Animations.PlayAttack(_attackData.attackName);
        }

        public override void Tick(float deltaTime)
        {
        }

        public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
        {
            base.AnimationTriggerEvent(triggerType);

            if(triggerType == AnimationTriggerType.FinishAttack)
            {
                //check for new combo
                if (_runner.AttackPressed)
                {
                    //next combo
                }
                else
                {
                    _runner.SetState(typeof(PlayerIdleState));
                }
            }

            if (triggerType != AnimationTriggerType.HitBox) return;

            var colliders = _attackData.Hit(_runner.transform, _runner.IsFacingRight);

            PerformDamage(colliders);
        }

        private void PerformDamage(Collider[] colliders)
        {
            if (colliders.Length <= 0) return;

            foreach (var col in colliders)
            {
                Debug.Log($"colliding with {col.gameObject.name}");
                // if (col.TryGetComponent(out Health health))
                // {
                //      health.TakeDamage(_attackData.Damage)
                // }
            }
        }

        public override void FixedTick(float fixedDeltaTime)
        {
        }

        public override void ChangeState()
        {
            if(_runner.Movement.sqrMagnitude != 0)
            {
                _runner.SetState(typeof(PlayerMove3DState));
            }
        }
    }
}

