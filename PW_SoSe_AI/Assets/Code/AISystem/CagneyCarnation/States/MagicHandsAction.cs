using System;
using System.Collections.Generic;
using AISystem.Acorns;
using Core.Utility;
using ProjectileSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AISystem.CagneyCarnation.States
{
	[CreateAssetMenu(menuName = "Cuphead/Bosses/CagneyCarnation/Attacks/MagicHands", fileName = "MagicHands", order = 0)]
	public class MagicHandsAction : EnemyAttackActionState
	{
		[SerializeField] private List<MagicHandsSpawnEntry> _projectilePrefabs = new List<MagicHandsSpawnEntry>();
		[SerializeField] private GameObject _spawnFX = default;
		[SerializeField] private AcornProjectileSpawnSettings _acornSettings = default;

		private Transform _magicHandsSpawn;
		
		public override void InitState()
		{
			base.InitState();

			_magicHandsSpawn = FindObjectOfType<MagicHandsSpawnMarker>().transform;
		}

		protected override void OnStateEnter(CagneyCarnationFsm fsm, Enemy enemy)
		{
			base.OnStateEnter(fsm, enemy);
			
			float roll = Random.value;
			enemy.Animator.SetTrigger(roll > 0.5f ? "CreateLoop" : "CreateSimple");

			EnemyAnimationEventReceiver.OnSpawnMagicHandsProjectile += Spawn;
		}

		protected override void OnStateExit(CagneyCarnationFsm fsm, Enemy enemy)
		{
			base.OnStateExit(fsm, enemy);

			EnemyAnimationEventReceiver.OnSpawnMagicHandsProjectile -= Spawn;
		}
		
		private void Spawn()
		{
			MagicHandsSpawnEntry pick = _projectilePrefabs.PickRandomWeighted(Random.value);
			GameObject spawnFX = Instantiate(_spawnFX);
			spawnFX.transform.position = _magicHandsSpawn.position;

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
			float startFlyingDelay = _acornSettings.MinMaxStartDelay.GetRandomBetween();
			Vector3 spawnOffset = Vector3.up * _acornSettings.DistanceBetweenAcorns * 1.5f;
			for (int i = 0; i < 4; i++)
			{
				AcornProjectileBehaviour acorn = (AcornProjectileBehaviour) Instantiate(pick.ProjectilePrefab);
				acorn.StartFlyingDelay = startFlyingDelay;
				acorn.transform.position = _magicHandsSpawn.position + spawnOffset;

				startFlyingDelay += _acornSettings.DelayBetweenAcorns;
				spawnOffset -= Vector3.up * _acornSettings.DistanceBetweenAcorns;
			}
		}

		private void SpawnBoomerang(MagicHandsSpawnEntry pick)
		{
			BaseProjectileBehaviour baseProjectileBehaviour = Instantiate(pick.ProjectilePrefab);
			baseProjectileBehaviour.transform.position = _magicHandsSpawn.position;
		}
	}

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

	public enum MagicHandsSpawnType
	{
		Boomerang,
		Acorns,
	}
}