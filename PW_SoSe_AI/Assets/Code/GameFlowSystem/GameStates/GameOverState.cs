using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace GameFlowSystem.GameStates
{
	public class GameOverState : MonoGameState
	{
		[SerializeField] private PlayableDirector _gameOverTimeline = default;
		
		public override void StateEnter()
		{
			_gameOverTimeline.Play();
			_gameOverTimeline.stopped += OnGameOverTimelineStopped;
		}

		private void OnGameOverTimelineStopped(PlayableDirector obj)
		{
			if (!obj.Equals(_gameOverTimeline))
			{
				return;
			}

			_gameOverTimeline.stopped -= OnGameOverTimelineStopped;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public override IGameState StateUpdate()
		{
			return this;
		}

		public override void StateExit()
		{
		}
	}
}