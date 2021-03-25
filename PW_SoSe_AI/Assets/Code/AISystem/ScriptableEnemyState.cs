using UnityEngine;

namespace AISystem
{
	/// <summary>
	/// 	Base state as a scriptable object.
	/// </summary>
	/// <typeparam name="TFsm">This is a generic type that allows you to specify the exact state machine at a later point in time</typeparam>
	/// <typeparam name="TState">This is a generic type that allows to specify the exact state that is going th be used later</typeparam>
	public abstract class ScriptableEnemyState<TFsm, TState> : ScriptableObject where TState : ScriptableEnemyState<TFsm, TState>
	{
		[SerializeField] private StateIdentifier _id;

		/// <summary>
		/// 	Has an id to uniquely identify each state
		/// </summary>
		public StateIdentifier Id => _id;
		
		/// <summary>
		/// 	Generic OnStateEnter method - concrete states will not have "TFsm" but for example "BossFsm", so we can access its values
		/// </summary>
		/// <param name="fsm">Type of the state machine</param>
		/// <param name="enemy">The enemy to operate on</param>
		public abstract void OnStateEnter(TFsm fsm, Enemy enemy);
		
		/// <summary>
		/// 	Generic OnStateExit method - concrete states will not have "TFsm" but for example "BossFsm", so we can access its values
		/// </summary>
		/// <param name="fsm">Type of the state machine</param>
		/// <param name="enemy">The enemy to operate on</param>
		public abstract void OnStateExit(TFsm fsm, Enemy enemy);
		
		/// <summary>
		/// 	Generic OnStateUpdate method - concrete states will not have "TFsm" but for example "BossFsm", so we can access its values
		/// 	Is going to be called in Unity.Update();
		/// </summary>
		/// <param name="fsm">Type of the state machine</param>
		/// <param name="enemy">The enemy to operate on</param>
		public abstract TState OnStateUpdate(TFsm fsm, Enemy enemy);

		/// <summary>
		/// 	Can do set up here if required. Potentially gets called from UnityEngine.Awake().
		/// </summary>
		public virtual void InitState()
		{
		}
	}
}