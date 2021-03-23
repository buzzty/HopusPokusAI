using AISystem;
using CharacterSystem;

namespace GameFlowSystem.GameStates
{
	public class MatchState : MonoGameState
	{
		private bool _playerDied;
		private bool _bossDied;

		public override void StateEnter()
		{
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
			if (_playerDied)
			{
				return GameManager.GameOverState;
			}

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