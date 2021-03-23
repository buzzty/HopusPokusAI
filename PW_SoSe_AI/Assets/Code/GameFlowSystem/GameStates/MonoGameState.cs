using Core.Utility;

namespace GameFlowSystem.GameStates
{
	public abstract class MonoGameState : CachedMonoBehaviour, IGameState
	{
		public abstract void StateEnter();
		public abstract IGameState StateUpdate();
		public abstract void StateExit();
	}
}