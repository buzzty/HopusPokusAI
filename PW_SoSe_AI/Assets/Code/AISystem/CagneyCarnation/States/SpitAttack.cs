using ProjectileSystem;
using UnityEngine;

namespace AISystem.CagneyCarnation.States
{
	/// <summary>
	/// 	Instantiates a <see cref="SpitProjectileBehaviour"/> to shoot at the player.
	/// </summary>
	[CreateAssetMenu(menuName = "Cuphead/Bosses/CagneyCarnation/Attacks/SpitAttack", fileName = "SpitAttack", order = 0)]
	public class SpitAttack : EnemyAttackActionState
	{
		/// <summary>
		/// 	Prefab for the projectile to spit
		/// </summary>
		[SerializeField] private SpitProjectileBehaviour _projectilePrefab = default;

		private Transform _spawnPoint;
		
		public override void InitState()
		{
			base.InitState();
			// find the spawn point of the spit attack
			_spawnPoint = FindObjectOfType<SpitProjectileSpawnPointMarker>().TransformCached;
		}

		protected override void OnStateEnter(CagneyCarnationFsm fsm, Enemy enemy)
		{
			base.OnStateEnter(fsm, enemy);

			// playback animation
			enemy.Animator.SetTrigger("Spit");
			// sub to event
			EnemyAnimationEventReceiver.OnSpawnSpitProjectile += Spawn;
		}

		protected override void OnStateExit(CagneyCarnationFsm fsm, Enemy enemy)
		{
			base.OnStateExit(fsm, enemy);
			EnemyAnimationEventReceiver.OnSpawnSpitProjectile -= Spawn;
		}

		private void Spawn()
		{
			// spawn proectiole and set its position
			Instantiate(_projectilePrefab).transform.position = _spawnPoint.position;
		}
	}
}