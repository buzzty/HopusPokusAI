using AISystem;
using CharacterSystem;

namespace GameFlowSystem.GameStates
{
	/// <summary>
	/// 	State for the match. Keep game loop up while we are in this.
	/// </summary>
	public class MatchState : MonoGameState
	{
		private bool _playerDied;
		private bool _bossDied;

		public override void StateEnter()
		{
			// listening to events to see if player/boss dies
			PlayerDamageable.PlayerDeath += OnPlayerDeath;
			Enemy.BossDeath += OnBossDeath;
		}

		private void OnBossDeath()
		{
			_bossDied = true;
		}

		private void OnPlayerDeath()
		{
			_playerDied = true;
		}

		public override IGameState StateUpdate()
		{
			// transition to next state if player is dead
			if (_playerDied)
			{
				return GameManager.GameOverState;
			}

			// transition to next state if boss is dead
			if (_bossDied)
			{
				return GameManager.KnockoutState;
			}

			return this;
		}

		public override void StateExit()
		{
			PlayerDamageable.PlayerDeath -= OnPlayerDeath;
			Enemy.BossDeath -= OnBossDeath;
		}
	}
}