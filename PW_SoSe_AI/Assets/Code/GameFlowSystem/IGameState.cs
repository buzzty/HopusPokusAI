namespace GameFlowSystem
{
	/// <summary>
	/// 	Interface for the game state - no need to pass in references as the gamemanager is statically available
	/// </summary>
	public interface IGameState
	{
		void StateEnter();
		IGameState StateUpdate();
		void StateExit();
	}
}