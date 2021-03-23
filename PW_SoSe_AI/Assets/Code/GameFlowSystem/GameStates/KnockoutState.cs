using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace GameFlowSystem.GameStates
{
	public class KnockoutState : MonoGameState
	{
		[SerializeField] private PlayableDirector _knockoutTimeline = default;
		
		public override void StateEnter()
		{
			_knockoutTimeline.Play();
			_knockoutTimeline.stopped += OnKnockoutTimelineStopped;
		}

		private void OnKnockoutTimelineStopped(PlayableDirector obj)
		{
			if (!obj.Equals(_knockoutTimeline))
			{
				return;
			}

			_knockoutTimeline.stopped -= OnKnockoutTimelineStopped;
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