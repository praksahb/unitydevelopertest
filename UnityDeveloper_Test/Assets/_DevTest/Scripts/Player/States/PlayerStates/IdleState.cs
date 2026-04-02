using UnityEngine;

namespace DevTest.Player
{
    public class IdleState : IBaseState
    {
        private PlayerStateManager manager;

        public IdleState()
        {
        }

        public void OnEnterState(PlayerStateManager manager)
        {
            manager.PlayerController.AnimController.PlayAnimation(PlayerAnimationState.Idle);

            manager.PlayerController.PlayerView.Rigidbody.linearVelocity = new Vector3(0, manager.PlayerController.PlayerView.Rigidbody.linearVelocity.y, 0);
        }

        public void OnUpdate(PlayerStateManager manager)
        {
            if (Mathf.Abs(manager.PlayerController.HorizontalInput) > 0.1f || Mathf.Abs(manager.PlayerController.VerticalInput) > 0.1f)
            {
                manager.ChangeState(manager.RunningState);
            }

            if (manager.PlayerController.JumpRequested && manager.PlayerController.IsGrounded)
            {
                manager.ChangeState(manager.InAirState);
            }
        }

        public void OnFixedUpdate(PlayerStateManager manager)
        {
            if (!manager.PlayerController.IsGrounded)
            {
                manager.ChangeState(manager.InAirState);
            }
        }

        public void OnExitState(PlayerStateManager manager) { }
    }
}
