using UnityEngine;

namespace AISystem.CagneyCarnation.Phases
{
	[CreateAssetMenu(menuName = "Cuphead/Bosses/CagneyCarnation/States/SpawnPhase", fileName = "SpawnPhase", order = 0)]
	public class SpawnPhase : AnimatorDrivenPhase
	{
		public override void OnStateEnter(EnemyPhaseFSM phaseFsm, Enemy enemy)
		{
			base.OnStateEnter(phaseFsm, enemy);
			enemy.Animator.SetTrigger("SetSpawn");
		}
	}
}