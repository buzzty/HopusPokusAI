using UnityEngine;
using Random = UnityEngine.Random;

namespace AISystem.CagneyCarnation.States
{
	[CreateAssetMenu(menuName = "Cuphead/Bosses/CagneyCarnation/Attacks/FaceAttack", fileName = "FaceAttack", order = 0)]
	public class FaceAttack : EnemyAttackActionState
	{
		[SerializeField] private AudioClip _faceAttackLoop;
		
		protected override void OnStateEnter(CagneyCarnationFsm fsm, Enemy enemy)
		{
			base.OnStateEnter(fsm, enemy);
			
			float roll = Random.value;
			// set animation trigger and play audio - state is driven by animation and will exit once anim is done
			enemy.Animator.SetTrigger(roll > 0.5f ? "FaceHigh" : "FaceLow");
			enemy.PlayAudio(_faceAttackLoop, true);
		}

		protected override void OnStateExit(CagneyCarnationFsm fsm, Enemy enemy)
		{
			base.OnStateExit(fsm, enemy);
			
			enemy.StopAudio();
		}
	}
}