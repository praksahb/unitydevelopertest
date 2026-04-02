using UnityEngine;

namespace DevTest.Player
{
    public class PlayerController
    {
        public PlayerModel PlayerModel { get; }
        public PlayerView PlayerView { get; }
        public PlayerStateManager StateManager { get; }

        public PlayerAnimationController AnimController { get; }

        // Input data
        public float HorizontalInput { get; private set; }
        public float VerticalInput { get; private set; }
        public bool JumpRequested { get; private set; }

        public bool IsGrounded => PlayerView.IsGrounded;

        public PlayerController(PlayerModel playerModel, PlayerView playerView, Transform spawnPoint)
        {
            PlayerModel = playerModel;
            PlayerView = Object.Instantiate(playerView, spawnPoint);
            
            // Link back to controller
            PlayerView.PlayerController = this;
            AnimController = PlayerView.PlayerAnimController;

            // Initialize the State Manager
            StateManager = new PlayerStateManager(this);
        }



        public void HandleUpdate()
        {
            // Cache Input
            HorizontalInput = Input.GetAxis("Horizontal");
            VerticalInput = Input.GetAxis("Vertical");
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                JumpRequested = true;
            }

            // take the state update last
            StateManager.Update(); 
        }

        public void HandleFixedUpdate()
        {
            // Fixed update logic is now handled by the PlayerStateManager component itself
            JumpRequested = false;

            // same for FixedUpdate
            StateManager.FixedUpdate();
        }

        // The View's Update calls HandleUpdate, but who calls HandleFixedUpdate now?
        // Let's re-add FixedUpdate to the View or just have the Controller called by the View.
        // Actually, the PlayerStateManager handles the CurrentState's FixedUpdate!
        // We just need to clear the input.
    }
}