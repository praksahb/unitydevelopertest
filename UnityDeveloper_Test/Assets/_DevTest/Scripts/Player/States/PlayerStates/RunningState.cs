using UnityEngine;

namespace DevTest.Player
{
    public class RunningState : IBaseState
    {
        public RunningState()
        {
        }

        public void OnEnterState(PlayerStateManager manager)
        {
            manager.PlayerController.AnimController.PlayAnimation(PlayerAnimationState.Running);
        }

        public void OnUpdate(PlayerStateManager manager)
        {
            if (Mathf.Abs(manager.PlayerController.HorizontalInput) < 0.1f && Mathf.Abs(manager.PlayerController.VerticalInput) < 0.1f)
            {
                manager.ChangeState(manager.IdleState);
            }

            if (manager.PlayerController.JumpRequested && manager.PlayerController.IsGrounded)
            {
                manager.ChangeState(manager.InAirState);
            }
        }

        public void OnFixedUpdate(PlayerStateManager manager)
        {
            MoveCharacter(manager);

            if (!manager.PlayerController.IsGrounded)
            {
                manager.ChangeState(manager.InAirState);
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

        public void OnExitState(PlayerStateManager manager) { }
    }
}
