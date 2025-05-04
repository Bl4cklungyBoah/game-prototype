using UnityEngine;

namespace Player
{
    public class PlayerAnimations  
    {
        private Animator _animator;

        //this needs to match the names on the Animator view
        private int PLAYER_IDLE = Animator.StringToHash("Idle");
        private int PLAYER_MOVE = Animator.StringToHash("Move");
        private int PLAYER_ROLL = Animator.StringToHash("Roll");

        private float _transitionDuration = .1f;

        public PlayerAnimations(Animator animator)
        {
            _animator = animator;
        }

        public void PlayIdle()
        {
            Debug.Log("Invoked PlayIdle function");
            _animator.CrossFade(PLAYER_IDLE, _transitionDuration);
        }

        public void PlayMove()
        {
            Debug.Log("Invoked PlayMove function");
            _animator.CrossFade(PLAYER_MOVE, _transitionDuration);
        }

        public void PlayRoll()
        {

            Debug.Log("Invoked PlayRoll function");
            _animator.CrossFade(PLAYER_ROLL, _transitionDuration);
        }

        public void PlayAttack(string attackName)
        {
            Debug.Log("Invoked PlayAttack function");
            _animator.CrossFade(attackName, _transitionDuration);
        }
    }
}

