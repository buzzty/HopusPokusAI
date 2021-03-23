namespace GameFlowSystem
{
	public interface IGameState
	{
		void StateEnter();
		IGameState StateUpdate();
		void StateExit();
	}
}