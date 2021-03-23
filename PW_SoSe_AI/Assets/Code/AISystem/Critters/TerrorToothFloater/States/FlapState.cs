using Core.Utility;
using UnityEngine;

namespace AISystem.Critters.TerrorToothFloater.States
{
	[CreateAssetMenu(menuName = "Cuphead/Critters/MiniFlower/States/FlapState", fileName = "MiniFlower_Flap", order = 0)]
	public class FlapState : MiniFlowerState
	{
		[SerializeField] [MinMaxFloat(2, 8f)]
		private MinMaxFloat _minMaxStateDuration = new MinMaxFloat(4f, 8f);
		[SerializeField] private float _flapSpeed = 2f;

		private MiniFlowerHeightMarker _heightMarker;
		private float _stateDuration;
		public override void InitState()
		{
			base.InitState();
			
			_heightMarker = FindObjectOfType<MiniFlowerHeightMarker>();
		}

		protected override void OnStateEnter(MiniFlower fsm, Enemy enemy)
		{
			base.OnStateEnter(fsm, enemy);
			fsm.FlapDuration = _minMaxStateDuration.GetRandomBetween();
				
			if (!enemy.Animator.GetCurrentAnimatorStateInfo(0).IsName("MiniFlower_FlapLoop"))
			{
				enemy.Animator.SetTrigger("Flap");
			}
		}

		protected override MiniFlowerState OnStateUpdate(MiniFlower fsm, Enemy enemy)
		{
			enemy.TransformCached.position += (fsm.ToLeft ? Vector3.left : Vector3.right) * (_flapSpeed * Time.deltaTime);
			if (_heightMarker.HitBorder(enemy.TransformCached.position))
			{
				fsm.ToLeft = !fsm.ToLeft;
			}
			
			fsm.FlapDuration -= Time.deltaTime;
			if (fsm.FlapDuration <= 0)
			{
				return fsm.ShootState;
			}
			
			return base.OnStateUpdate(fsm, enemy);
		}
	}
}