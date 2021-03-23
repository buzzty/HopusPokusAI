using System;
using Core;
using GameFlowSystem.GameStates;

namespace GameFlowSystem
{
	public class GameManager : MonoSingleton<GameManager>
	{
		public static MatchIntroState MatchIntroState => _matchIntroState;
		public static MatchState MatchState => _matchState;
		public static KnockoutState KnockoutState => _knockoutState;
		public static GameOverState GameOverState => _gameOverState;
		
		private static MatchIntroState _matchIntroState;
		private static MatchState _matchState;
		private static KnockoutState _knockoutState;
		private static GameOverState _gameOverState;
		
		private IGameState _currentState;

		public bool IsGameOver => _currentState.Equals(_gameOverState);
		public bool IsMatch => _currentState.Equals(_matchState);

		protected override void Awake()
		{
			base.Awake();
			
			_matchIntroState = GetComponentInChildren<MatchIntroState>();
			_matchState = GetComponentInChildren<MatchState>();
			_knockoutState = GetComponentInChildren<KnockoutState>();
			_gameOverState = GetComponentInChildren<GameOverState>();
			
			_currentState = MatchIntroState;
		}

		private void Start()
		{
			_currentState.StateEnter();
		}

		private void Update()
		{
			IGameState nextState = _currentState.StateUpdate();
			if (nextState != _currentState)
			{
				_currentState.StateExit();
				_currentState = nextState;
				_currentState.StateEnter();
			}
		}
	}
}