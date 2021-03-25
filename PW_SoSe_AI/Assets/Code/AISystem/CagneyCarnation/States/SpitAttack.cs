using ProjectileSystem;
using UnityEngine;

namespace AISystem.CagneyCarnation.States
{
	[CreateAssetMenu(menuName = "Cuphead/Bosses/CagneyCarnation/Attacks/SpitAttack", fileName = "SpitAttack", order = 0)]
	public class SpitAttack : HPEnemyAttackActionState
	{
		[SerializeField] private SpitProjectileBehaviour _projectilePrefab = default;

		private Transform _spawnPoint;
		
		public override void InitState()
		{
			base.InitState();
			_spawnPoint = FindObjectOfType<SpitProjectileSpawnPointMarker>().TransformCached;
		}

		protected override void OnStateEnter(CagneyCarnationFsm fsm, Enemy enemy)
		{
			base.OnStateEnter(fsm, enemy);

			enemy.Animator.SetTrigger("Spit");
			EnemyAnimationEventReceiver.OnSpawnSpitProjectile += Spawn;
		}

		protected override void OnStateExit(CagneyCarnationFsm fsm, Enemy enemy)
		{
			base.OnStateExit(fsm, enemy);
			EnemyAnimationEventReceiver.OnSpawnSpitProjectile -= Spawn;
		}

		private void Spawn()
		{
			Instantiate(_projectilePrefab).transform.position = _spawnPoint.position;
		}
	}
}