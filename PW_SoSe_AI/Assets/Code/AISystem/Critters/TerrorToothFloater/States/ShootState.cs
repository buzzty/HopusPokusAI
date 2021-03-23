using ProjectileSystem;
using UnityEngine;

namespace AISystem.Critters.TerrorToothFloater.States
{
	[CreateAssetMenu(menuName = "Cuphead/Critters/MiniFlower/States/Shoot", fileName = "MiniFlower_Shoot", order = 0)]
	public class ShootState : MiniFlowerState, IAnimatorDriveable
	{
		[SerializeField] private BaseProjectileBehaviour _projectilePrefab = default;
		
		private bool _isAnimationDone;
		private bool _requireSpawn;
		public bool IsAnimationDone => _isAnimationDone;
		public void AnimationDone()
		{
			_isAnimationDone = true;
		}

		public override void InitState()
		{
			base.InitState();

			_isAnimationDone = false;
		}

		protected override void OnStateEnter(MiniFlower fsm, Enemy enemy)
		{
			base.OnStateEnter(fsm, enemy);
			enemy.Animator.SetTrigger("Shoot");
			_isAnimationDone = false;
			_requireSpawn = false;

			EnemyAnimationEventReceiver.OnMiniFlowerShoot += ScheduleProjectileShot;
		}

		protected override void OnStateExit(MiniFlower fsm, Enemy enemy)
		{
			base.OnStateExit(fsm, enemy);
			EnemyAnimationEventReceiver.OnMiniFlowerShoot -= ScheduleProjectileShot;
		}

		private void ScheduleProjectileShot()
		{
			_requireSpawn = true;
		}
		
		private void Shoot(Enemy enemy)
		{
			_requireSpawn = false;
			Instantiate(_projectilePrefab, enemy.TransformCached.position, Quaternion.identity);
		}

		protected override MiniFlowerState OnStateUpdate(MiniFlower fsm, Enemy enemy)
		{
			if (_requireSpawn)
			{
				Shoot(enemy);
			}
			
			if (IsAnimationDone)
			{
				return fsm.FlapState;
			}
			
			return base.OnStateUpdate(fsm, enemy);
		}
	}
}