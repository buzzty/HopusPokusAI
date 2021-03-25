namespace AISystem.CagneyCarnation
{
	/// <summary>
	/// 	Base class for all cagney carnation states
	/// </summary>
	public abstract class CagneyCarnationState : ScriptableEnemyState<EnemyActionFSM<CagneyCarnationState>, CagneyCarnationState>
	{
		/// <summary>
		/// 	This FSM is now a concrete implementation of the TFSM generic type parameter from the class above.
		/// 	In here, we can access the state machine that controls the boss
		/// </summary>
		/// <param name="fsm">State machine that currently drives the boss</param>
		/// <param name="enemy">enemy that is being controlled by the state machine</param>
		/// <returns>The new state to transition to</returns>
		public override CagneyCarnationState OnStateUpdate(EnemyActionFSM<CagneyCarnationState> fsm, Enemy enemy)
		{
			// to properly access the fsm, we cast it to the specific type and forward the call to an internal method
			return OnStateUpdate(fsm as CagneyCarnationFsm, enemy);
		}

		/// <summary>
		/// 	This FSM is now a concrete implementation of the TFSM generic type parameter from the class above.
		/// 	In here, we can access the state machine that controls the boss
		/// </summary>
		/// <param name="fsm">State machine that currently drives the boss</param>
		/// <param name="enemy">enemy that is being controlled by the state machine</param>
		public override void OnStateEnter(EnemyActionFSM<CagneyCarnationState> fsm, Enemy enemy)
		{
			// to properly access the fsm, we cast it to the specific type and forward the call to an internal method
			OnStateEnter(fsm as CagneyCarnationFsm, enemy);
		}
		
		/// <summary>
		/// 	This FSM is now a concrete implementation of the TFSM generic type parameter from the class above.
		/// 	In here, we can access the state machine that controls the boss
		/// </summary>
		/// <param name="fsm">State machine that currently drives the boss</param>
		/// <param name="enemy">enemy that is being controlled by the state machine</param>
		public override void OnStateExit(EnemyActionFSM<CagneyCarnationState> fsm, Enemy enemy)
		{
			// to properly access the fsm, we cast it to the specific type and forward the call to an internal method
			OnStateExit(fsm as CagneyCarnationFsm, enemy);
		}

		/// <summary>
		/// 	Gives Access to the CagneyCarnationFsM and allows us to define state transitions and do other update behaviour inside of the state.
		/// </summary>
		/// <param name="fsm">State machine that currently drives the boss</param>
		/// <param name="enemy">enemy that is being controlled by the state machine</param>
		/// <returns>The new state to transition to</returns>
		protected virtual CagneyCarnationState OnStateUpdate(CagneyCarnationFsm fsm, Enemy enemy)
		{
			// as this is the base state, stay in the current state, in case we get all the way up here.
			return this;
		}

		// nothing to do in enter in base state
		protected virtual void OnStateEnter(CagneyCarnationFsm fsm, Enemy enemy)
		{
		}

		// nothing to do in exit in base state
		protected virtual void OnStateExit(CagneyCarnationFsm fsm, Enemy enemy)
		{
		}
	}
}