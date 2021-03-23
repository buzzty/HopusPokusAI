namespace AISystem.CagneyCarnation
{
	public abstract class CagneyCarnationState : ScriptableEnemyState<EnemyActionFSM<CagneyCarnationState>, CagneyCarnationState>
	{
		public override CagneyCarnationState OnStateUpdate(EnemyActionFSM<CagneyCarnationState> fsm, Enemy enemy)
		{
			return OnStateUpdate(fsm as CagneyCarnationFsm, enemy);
		}

		public override void OnStateEnter(EnemyActionFSM<CagneyCarnationState> fsm, Enemy enemy)
		{
			OnStateEnter(fsm as CagneyCarnationFsm, enemy);
		}

		public override void OnStateExit(EnemyActionFSM<CagneyCarnationState> fsm, Enemy enemy)
		{
			OnStateExit(fsm as CagneyCarnationFsm, enemy);
		}

		protected virtual CagneyCarnationState OnStateUpdate(CagneyCarnationFsm fsm, Enemy enemy)
		{
			return this;
		}

		protected virtual void OnStateEnter(CagneyCarnationFsm fsm, Enemy enemy)
		{
		}

		protected virtual void OnStateExit(CagneyCarnationFsm fsm, Enemy enemy)
		{
		}
	}
}