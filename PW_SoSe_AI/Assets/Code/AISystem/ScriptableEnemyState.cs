using UnityEngine;

namespace AISystem
{
	public abstract class ScriptableEnemyState<TFsm, TState> : ScriptableObject where TState : ScriptableEnemyState<TFsm, TState>
	{
		[SerializeField] private StateIdentifier _id;

		public StateIdentifier Id => _id;
		
		public abstract void OnStateEnter(TFsm fsm, Enemy enemy);
		public abstract void OnStateExit(TFsm fsm, Enemy enemy);
		public abstract TState OnStateUpdate(TFsm fsm, Enemy enemy);

		public virtual void InitState()
		{
		}
	}
}