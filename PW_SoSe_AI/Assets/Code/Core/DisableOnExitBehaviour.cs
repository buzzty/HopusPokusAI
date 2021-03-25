using UnityEngine;

namespace Core
{
	/// <summary>
	/// 	This class is a behaviour that gets attached to an animator state.
	/// 	We can use this to our advantage in order to disable gameobjects, when they exit certain animation states (see below)
	/// </summary>
	public class DisableOnExitBehaviour : StateMachineBehaviour
	{
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			animator.gameObject.SetActive(false);
		}

		public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}
	}
}