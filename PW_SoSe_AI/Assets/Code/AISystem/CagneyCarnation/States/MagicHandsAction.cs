using System;
using System.Collections.Generic;
using AISystem.Acorns;
using Core.Utility;
using ProjectileSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AISystem.CagneyCarnation.States
{
	/// <summary>
	/// 	Executes the Magic Hands attack which can spawn acorns/boomerang
	/// </summary>
	[CreateAssetMenu(menuName = "Cuphead/Bosses/CagneyCarnation/Attacks/MagicHands", fileName = "MagicHands", order = 0)]
	public class MagicHandsAction : EnemyAttackActionState
	{
		/// <summary>
		/// List of potential spawn objects - we can assign all of them a <see cref="BaseProjectileBehaviour"/> and give it a weight. Whenever this attack gets executed, a potential projectile gets picked based on its weight.
		/// </summary>
		[SerializeField] private List<MagicHandsSpawnEntry> _projectilePrefabs = new List<MagicHandsSpawnEntry>();
		/// <summary>
		/// Play this FX at the spawn position, just as in the game
		/// </summary>
		[SerializeField] private GameObject _spawnFX = default;
		/// <summary>
		/// Settings detailing how the acorns are spawned (distance, delay between them, ...)
		/// </summary>
		[SerializeField] private AcornProjectileSpawnSettings _acornSettings = default;

		private Transform _magicHandsSpawn;
		
		public override void InitState()
		{
			base.InitState();

			// find spawn pos
			_magicHandsSpawn = FindObjectOfType<MagicHandsSpawnMarker>().transform;
		}

		protected override void OnStateEnter(CagneyCarnationFsm fsm, Enemy enemy)
		{
			base.OnStateEnter(fsm, enemy);
			
			// do the long or simple version?
			float roll = Random.value;
			enemy.Animator.SetTrigger(roll > 0.5f ? "CreateLoop" : "CreateSimple");

			// this event gets called by an animation event, because the exact timing of when to spawn the projectiles is driven by that
			EnemyAnimationEventReceiver.OnSpawnMagicHandsProjectile += Spawn;
		}

		protected override void OnStateExit(CagneyCarnationFsm fsm, Enemy enemy)
		{
			base.OnStateExit(fsm, enemy);

			EnemyAnimationEventReceiver.OnSpawnMagicHandsProjectile -= Spawn;
		}
		
		private void Spawn()
		{
			// pick a randomly but weighted projectile prefab and spawn it
			MagicHandsSpawnEntry pick = _projectilePrefabs.PickRandomWeighted(Random.value);
			GameObject spawnFX = Instantiate(_spawnFX);
			spawnFX.transform.position = _magicHandsSpawn.position;

			// switch on spawn type because it requires different handling
			switch (pick.SpawnType)
			{
				case MagicHandsSpawnType.Boomerang:
					SpawnBoomerang(pick);
					break;
				case MagicHandsSpawnType.Acorns:
					SpawnAcorns(pick);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void SpawnAcorns(MagicHandsSpawnEntry pick)
		{
			// how long to delay the start of the acorns flying away
			float startFlyingDelay = _acornSettings.MinMaxStartDelay.GetRandomBetween();
			// offset between acorns
			Vector3 spawnOffset = Vector3.up * _acornSettings.DistanceBetweenAcorns * 1.5f;
			for (int i = 0; i < 4; i++)
			{
				// spawn the acorn prefab
				AcornProjectileBehaviour acorn = (AcornProjectileBehaviour) Instantiate(pick.ProjectilePrefab);
				// set start delay
				acorn.StartFlyingDelay = startFlyingDelay;
				// offset them from another
				acorn.transform.position = _magicHandsSpawn.position + spawnOffset;

				startFlyingDelay += _acornSettings.DelayBetweenAcorns;
				spawnOffset -= Vector3.up * _acornSettings.DistanceBetweenAcorns;
			}
		}

		private void SpawnBoomerang(MagicHandsSpawnEntry pick)
		{
			// simply spawn the boomerang at the spawn pos, then let it handle its behaviour itself
			BaseProjectileBehaviour baseProjectileBehaviour = Instantiate(pick.ProjectilePrefab);
			baseProjectileBehaviour.transform.position = _magicHandsSpawn.position;
		}
	}

	/// <summary>
	/// 	Class that allows us to associate different projectiles with a weight and assign them a <see cref="MagicHandsSpawnType"/>
	/// </summary>
	[Serializable]
	public class MagicHandsSpawnEntry : IWeightable
	{
		[SerializeField] private BaseProjectileBehaviour _projectilePrefab = default;
		[SerializeField] private int _weight = 1;
		[SerializeField] private MagicHandsSpawnType _spawnType = MagicHandsSpawnType.Boomerang;
		
		public int Weight => _weight;
		public BaseProjectileBehaviour ProjectilePrefab => _projectilePrefab;
		public MagicHandsSpawnType SpawnType => _spawnType;
	}
	
	/// <summary>
	/// 	Cagney Carnation can spawn either a boomerang or acorns - this wraps the possibilites as something more readable.
	/// </summary>
	public enum MagicHandsSpawnType
	{
		Boomerang,
		Acorns,
	}
}