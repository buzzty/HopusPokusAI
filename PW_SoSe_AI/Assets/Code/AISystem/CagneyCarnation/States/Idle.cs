using UnityEngine;

namespace AISystem.CagneyCarnation.States
{
	[CreateAssetMenu(menuName = "Cuphead/Bosses/CagneyCarnation/Attacks/IdleActionPhase01", fileName = "IdleActionPhase01", order = 0)]
	public class Idle : CagneyCarnationState
	{
		protected override void OnStateEnter(CagneyCarnationFsm fsm, Enemy enemy)
		{
			base.OnStateEnter(fsm, enemy);
			
			enemy.Animator.SetTrigger("Idle");
		}

		protected override CagneyCarnationState OnStateUpdate(CagneyCarnationFsm fsm, Enemy enemy)
		{
			if (!fsm.IsOnGlobalCooldown)
			{
				return fsm.GetNextState(enemy);
			}
			
			return fsm.Idle;
		}
	}
}