namespace AISystem.Critters.TerrorToothFloater.States
{
	public abstract class MiniFlowerState : ScriptableEnemyState<EnemyActionFSM<MiniFlowerState>, MiniFlowerState>
	{
		public override void OnStateEnter(EnemyActionFSM<MiniFlowerState> fsm, Enemy enemy)
		{
			OnStateEnter(fsm as MiniFlower, enemy);
		}

		public override void OnStateExit(EnemyActionFSM<MiniFlowerState> fsm, Enemy enemy)
		{
			OnStateExit(fsm as MiniFlower, enemy);
		}

		public override MiniFlowerState OnStateUpdate(EnemyActionFSM<MiniFlowerState> fsm, Enemy enemy)
		{
			return OnStateUpdate(fsm as MiniFlower, enemy);
		}
		
		protected virtual void OnStateEnter(MiniFlower fsm, Enemy enemy)
		{
		}

		protected virtual void OnStateExit(MiniFlower fsm, Enemy enemy)
		{
		}

		protected virtual MiniFlowerState OnStateUpdate(MiniFlower fsm, Enemy enemy)
		{
			return this;
		}
	}
}