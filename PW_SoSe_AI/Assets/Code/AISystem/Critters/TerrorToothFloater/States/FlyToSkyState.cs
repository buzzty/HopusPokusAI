using UnityEngine;

namespace AISystem.Critters.TerrorToothFloater.States
{
	[CreateAssetMenu(menuName = "Cuphead/Critters/MiniFlower/States/FlyToSky", fileName = "MiniFlower_FlyToSky", order = 0)]
	public class FlyToSkyState : MiniFlowerState
	{
		[SerializeField] private float _flyUpwardsSpeed = 1.5f;
		
		private Transform _heightPoint;
		
		public override void InitState()
		{
			base.InitState();
			_heightPoint = FindObjectOfType<MiniFlowerHeightMarker>().TransformCached;
		}

		protected override void OnStateEnter(MiniFlower fsm, Enemy enemy)
		{
			base.OnStateEnter(fsm, enemy);
			enemy.Animator.SetTrigger("Flap");
		}

		protected override void OnStateExit(MiniFlower fsm, Enemy enemy)
		{
			base.OnStateExit(fsm, enemy);

			
			enemy.IsInvincible = false;
		}

		protected override MiniFlowerState OnStateUpdate(MiniFlower fsm, Enemy enemy)
		{
			enemy.TransformCached.position += Vector3.up * (_flyUpwardsSpeed * Time.deltaTime);
			if (enemy.TransformCached.position.y >= _heightPoint.position.y)
			{
				return fsm.FlapState;
			}
			
			return base.OnStateUpdate(fsm, enemy);
		}
	}
}