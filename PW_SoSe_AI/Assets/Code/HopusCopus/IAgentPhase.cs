namespace AISystem.HopusCopus
{
    public interface IAgentPhase
    {
        StateIdentifier Id { get; }

        void OnStateEnter(EnemyPhaseFSM phaseFSM, Enemy enemy);
        void OnStateExit(EnemyPhaseFSM phaseFSM, Enemy enemy);
        bool OnStateUpdate(EnemyPhaseFSM phaseFSM, Enemy enemy);
    }
}
