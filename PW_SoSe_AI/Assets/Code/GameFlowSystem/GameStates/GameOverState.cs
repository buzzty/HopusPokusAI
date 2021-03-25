using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace GameFlowSystem.GameStates
{
	/// <summary>
	/// 	Class to control the game over state
	/// </summary>
	public class GameOverState : MonoGameState
	{
		[SerializeField] private PlayableDirector _gameOverTimeline = default;
		
		public override void StateEnter()
		{
			// Play a timeline
			_gameOverTimeline.Play();
			_gameOverTimeline.stopped += OnGameOverTimelineStopped;
		}

		private void OnGameOverTimelineStopped(PlayableDirector obj)
		{
			if (!obj.Equals(_gameOverTimeline))
			{
				return;
			}

			// game over timeline done - reload the scene
			_gameOverTimeline.stopped -= OnGameOverTimelineStopped;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public override IGameState StateUpdate()
		{
			// game is done, stay in this state until timeline terminates
			return this;
		}

		public override void StateExit()
		{
		}
	}
}