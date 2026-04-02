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
            // Decouple Arrow Keys from movement by explicitly polling WASD
            float h = 0f;
            if (Input.GetKey(KeyCode.A)) h -= 1f;
            if (Input.GetKey(KeyCode.D)) h += 1f;
            HorizontalInput = h;

            float v = 0f;
            if (Input.GetKey(KeyCode.S)) v -= 1f;
            if (Input.GetKey(KeyCode.W)) v += 1f;
            VerticalInput = v;
            
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

            StateManager.FixedUpdate();
        }
    }
}