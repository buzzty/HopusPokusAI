namespace AISystem.HopusPocus
{
    public abstract class HopusPocusState : ScriptableEnemyState<HPEnemyActionFSM<HopusPocusState>, HopusPocusState>
    {
        public override HopusPocusState OnStateUpdate(HPEnemyActionFSM<HopusPocusState> fsm, Enemy enemy)
        {
            return OnStateUpdate(fsm as HopusPocusFSM, enemy);
        }

        public override void OnStateEnter(HPEnemyActionFSM<HopusPocusState> fsm, Enemy enemy)
        {
            UnityEngine.Debug.Log("Calling On State Enter " + this);
            OnStateEnter(fsm as HopusPocusFSM, enemy);
        }

        public override void OnStateExit(HPEnemyActionFSM<HopusPocusState> fsm, Enemy enemy)
        {
            OnStateExit(fsm as HopusPocusFSM, enemy);
        }

        protected virtual HopusPocusState OnStateUpdate(HopusPocusFSM fsm, Enemy enemy)
        {
            return this;
        }

        protected virtual void OnStateEnter(HopusPocusFSM fsm, Enemy enemy)
        {
        }

        protected virtual void OnStateExit(HopusPocusFSM fsm, Enemy enemy)
        {
        }
    }
}
