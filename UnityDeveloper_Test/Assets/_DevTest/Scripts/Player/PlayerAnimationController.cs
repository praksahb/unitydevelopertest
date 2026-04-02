using UnityEngine;

namespace DevTest.Player
{
    public enum PlayerAnimationState
    {
        Idle,
        Running,
        InAir
    }

    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _crossFadeDuration = 0.2f;

        private readonly int IDLE = Animator.StringToHash("Idle");
        private readonly int RUNNING = Animator.StringToHash("Running");
        private readonly int IN_AIR = Animator.StringToHash("Falling_Idle");

        private PlayerAnimationState _currentAnimationState;

        public void PlayAnimation(PlayerAnimationState state)
        {
            if (_animator == null) return;
            if (_currentAnimationState == state) return;

            DevLog.Info($"[Animation] Playing State: {state}");

            int targetHash = 0;
            switch (state)
            {
                case PlayerAnimationState.Idle: targetHash = IDLE; break;
                case PlayerAnimationState.Running: targetHash = RUNNING; break;
                case PlayerAnimationState.InAir: targetHash = IN_AIR; break;
            }

            _animator.CrossFade(targetHash, _crossFadeDuration);
            _currentAnimationState = state;
        }
    }
}