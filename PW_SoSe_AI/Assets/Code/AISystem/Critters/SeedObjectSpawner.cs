using System;
using System.Collections.Generic;
using AISystem.Vines;
using Core.Utility;
using ProjectileSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AISystem.Critters
{
	public class SeedObjectSpawner : CachedMonoBehaviour
	{
		[SerializeField] private AnimationCurve _miniFlowerSpawnProbabilityCurve = default;
		[SerializeField] private VineCritterSpawner _vineCritterSpawnerPrefab;

		private Dictionary<SeedType, int> _seedTypeAmountMapping = new Dictionary<SeedType, int>();
		
		protected override void Awake()
		{
			base.Awake();

			ClearSeedTypeMapping();
		}

		private void Start()
		{
			SeedProjectileBehaviour.OnHitGround += SpawnVineForSeed;
			Enemy.EnemyDeath += OnEnemyDeath;
		}

		private void OnEnemyDeath(Enemy obj)
		{
			SeedTypeProvider seedTypeProvider = obj.GetComponent<SeedTypeProvider>();
			if (seedTypeProvider != null)
			{
				_seedTypeAmountMapping[seedTypeProvider.Type]--;
			}
		}

		private void OnDestroy()
		{
			SeedProjectileBehaviour.OnHitGround -= SpawnVineForSeed;
			Enemy.EnemyDeath -= OnEnemyDeath;
		}

		private void ClearSeedTypeMapping()
		{
			_seedTypeAmountMapping.Clear();
			foreach (SeedType value in Enum.GetValues(typeof(SeedType)))
			{
				_seedTypeAmountMapping.Add(value, 0);
			}
		}

		private void SpawnVineForSeed(SeedType type, Transform tr)
		{
			if (type == SeedType.ToothyTerrorFloater)
			{
				int currentFloaters = _seedTypeAmountMapping[SeedType.ToothyTerrorFloater];
				float probabilityForNewFloater = _miniFlowerSpawnProbabilityCurve.Evaluate(currentFloaters);
				float roll = Random.value;
				type = roll > probabilityForNewFloater ? type : SeedType.ToothyTerror;
				
				
				Debug.Log($"Originally Spawning {SeedType.ToothyTerrorFloater}. Current floaters {currentFloaters} - Probability for new {probabilityForNewFloater} - roll {roll}");
			}
			
			Debug.Log($"Spawning {type}");
			
			VineCritterSpawner vineCritter = Instantiate(_vineCritterSpawnerPrefab, transform);
			vineCritter.TransformCached.position = tr.position;
			vineCritter.SetVineSeedType(type);

			_seedTypeAmountMapping[type]++;
		}
	}
}