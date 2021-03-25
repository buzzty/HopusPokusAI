using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace GameFlowSystem.GameStates
{
	/// <summary>
	/// 	Knockout State - player won
	/// </summary>
	public class KnockoutState : MonoGameState
	{
		[SerializeField] private PlayableDirector _knockoutTimeline = default;
		
		public override void StateEnter()
		{
			// play a timeline
			_knockoutTimeline.Play();
			_knockoutTimeline.stopped += OnKnockoutTimelineStopped;
		}

		private void OnKnockoutTimelineStopped(PlayableDirector obj)
		{
			if (!obj.Equals(_knockoutTimeline))
			{
				return;
			}

			// timeline has stopped - we can reload the scene
			_knockoutTimeline.stopped -= OnKnockoutTimelineStopped;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public override IGameState StateUpdate()
		{
			// final state, we terminate here
			return this;
		}

		public override void StateExit()
		{
		}
	}
}