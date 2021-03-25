namespace AISystem.HopusPocus
{
    public interface HPIAgentPhase
    {
        StateIdentifier Id { get; }

        void OnStateEnter(EnemyPhaseFSM phaseFSM, Enemy enemy);
        void OnStateExit(EnemyPhaseFSM phaseFSM, Enemy enemy);
        bool OnStateUpdate(EnemyPhaseFSM phaseFSM, Enemy enemy);
    }
}
