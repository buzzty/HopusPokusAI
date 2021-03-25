using UnityEngine;
using UnityEngine.Playables;

namespace GameFlowSystem.GameStates
{
	/// <summary>
	/// 	State for the start of the match.
	/// </summary>
	public class MatchIntroState : MonoGameState
	{
		[SerializeField] private PlayableDirector _gameIntroTimeline = default;

		private bool _readySignalReceived;
		
		public override void StateEnter()
		{
			// Play intro timeline
			_gameIntroTimeline.Play();
			_readySignalReceived = false;
		}

		public override IGameState StateUpdate()
		{
			// timeline has sent a signal, so we can go to the next state
			if (_readySignalReceived)
			{
				// static access to the match state
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