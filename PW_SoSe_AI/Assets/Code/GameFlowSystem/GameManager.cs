using System;
using Core;
using GameFlowSystem.GameStates;

namespace GameFlowSystem
{
	/// <summary>
	/// 	This class controls the flow of the game. It implements a simple version of the state pattern and serves as a state machine.
	/// </summary>
	public class GameManager : MonoSingleton<GameManager>
	{
		// statically define all states, so we can access them everywhere
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
			
			// all states are monobehaviours, nested beneath our GameManager in this case - find them
			_matchIntroState = GetComponentInChildren<MatchIntroState>();
			_matchState = GetComponentInChildren<MatchState>();
			_knockoutState = GetComponentInChildren<KnockoutState>();
			_gameOverState = GetComponentInChildren<GameOverState>();
			
			// assign initial state
			_currentState = MatchIntroState;
		}

		private void Start()
		{
			// states are doing their own work in awake, so only call StateEnter in start
			_currentState.StateEnter();
		}

		private void Update()
		{
			// state machine pattern
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