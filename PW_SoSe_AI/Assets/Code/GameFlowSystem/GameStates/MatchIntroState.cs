using UnityEngine;
using UnityEngine.Playables;

namespace GameFlowSystem.GameStates
{
	public class MatchIntroState : MonoGameState
	{
		[SerializeField] private PlayableDirector _gameIntroTimeline = default;

		private bool _readySignalReceived;
		
		public override void StateEnter()
		{
			_gameIntroTimeline.Play();
			_readySignalReceived = false;
		}

		public override IGameState StateUpdate()
		{
			if (_readySignalReceived)
			{
				return GameManager.MatchState;
			}
			
			return this;
		}

		public void ReceiveReadySignal()
		{
			_readySignalReceived = true;
		}

		public override void StateExit()
		{
			_readySignalReceived = false;
		}
	}
}