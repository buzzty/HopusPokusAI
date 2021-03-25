namespace AISystem.HopusPocus
{
    public interface HPIAgentPhase
    {
        StateIdentifier Id { get; }

        void OnStateEnter(HPEnemyPhaseFSM phaseFSM, Enemy enemy);
        void OnStateExit(HPEnemyPhaseFSM phaseFSM, Enemy enemy);
        bool OnStateUpdate(HPEnemyPhaseFSM phaseFSM, Enemy enemy);
    }
}
