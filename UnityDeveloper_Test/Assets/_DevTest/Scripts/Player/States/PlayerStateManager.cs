using UnityEngine;

namespace DevTest.Player
{
    public class PlayerStateManager
    {
        public PlayerController PlayerController { get; }
        private IBaseState _currentState;

        public IdleState IdleState { get; } = new IdleState();
        public RunningState RunningState { get; } = new RunningState();
        public InAirState InAirState { get; } = new InAirState();

        public PlayerStateManager(PlayerController playerController)
        {
            PlayerController = playerController;

            // starting state
            ChangeState(IdleState);
        }

        public void Update()
        {
            _currentState?.OnUpdate(this);
        }

        public void FixedUpdate()
        {
            _currentState?.OnFixedUpdate(this);
        }

        public void ChangeState(IBaseState newState)
        {
            _currentState?.OnExitState(this);
            _currentState = newState;
            _currentState?.OnEnterState(this);
        }

        //public string GetCurrentStateName() => _currentState?.GetType().Name;
    }
}
