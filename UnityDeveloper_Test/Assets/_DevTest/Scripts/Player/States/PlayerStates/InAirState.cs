using UnityEngine;

namespace DevTest.Player
{
    public class InAirState : IBaseState
    {
        private bool _isJumping;

        public InAirState() { }

        public void OnEnterState(PlayerStateManager manager)
        {
            manager.PlayerController.AnimController.PlayAnimation(PlayerAnimationState.InAir);
            
            // If we are entering this state and a jump was requested, apply force
            if (manager.PlayerController.JumpRequested)
            {
                ApplyJumpForce(manager);
                _isJumping = true;
            }
        }

        private void ApplyJumpForce(PlayerStateManager manager)
        {
            Vector3 currentVelocity = manager.PlayerController.PlayerView.Rigidbody.linearVelocity;
            currentVelocity.y = manager.PlayerController.PlayerModel.JumpForce;
            manager.PlayerController.PlayerView.Rigidbody.linearVelocity = currentVelocity;
        }

        public void OnUpdate(PlayerStateManager manager) { }

        public void OnFixedUpdate(PlayerStateManager manager)
        {
            MoveCharacter(manager);

            if (manager.PlayerController.PlayerView.IsGrounded && manager.PlayerController.PlayerView.Rigidbody.linearVelocity.y <= 0)
            {
                if (manager.PlayerController.HorizontalInput == 0 && manager.PlayerController.VerticalInput == 0)
                {
                    manager.ChangeState(manager.IdleState);
                }
                else
                {
                    manager.ChangeState(manager.RunningState);
                }
            }
        }

        private void MoveCharacter(PlayerStateManager manager)
        {
            Vector3 moveDirection = new Vector3(manager.PlayerController.HorizontalInput, 0, manager.PlayerController.VerticalInput).normalized;
            Vector3 velocity = moveDirection * manager.PlayerController.PlayerModel.Speed;
            velocity.y = manager.PlayerController.PlayerView.Rigidbody.linearVelocity.y;
            manager.PlayerController.PlayerView.Rigidbody.linearVelocity = velocity;

            if (moveDirection != Vector3.zero)
            {
                manager.PlayerController.PlayerView.transform.forward = moveDirection;
            }
        }

        public void OnExitState(PlayerStateManager manager)
        {
            _isJumping = false;
        }
    }
}
