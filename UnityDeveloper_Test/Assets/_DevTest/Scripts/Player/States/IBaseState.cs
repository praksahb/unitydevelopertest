namespace DevTest.Player
{
    public interface IBaseState
    {
        void OnEnterState(PlayerStateManager manager);
        void OnUpdate(PlayerStateManager manager);
        void OnFixedUpdate(PlayerStateManager manager);
        void OnExitState(PlayerStateManager manager);
    }
}
