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
		// Animation curve is used to map probability to spawn another floater -> amount of floaters present
		[SerializeField] private AnimationCurve _miniFlowerSpawnProbabilityCurve = default;
		[SerializeField] private VineCritterSpawner _vineCritterSpawnerPrefab;

		// keep trakc of game entitiies spawned per seed type.
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

		/// <summary>
		/// 	Spawns a ToothyTerrorFlaoter or just a ToothyTerror based on its SeedType.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="tr"></param>
		private void SpawnVineForSeed(SeedType type, Transform tr)
		{
			if (type == SeedType.ToothyTerrorFloater)
			{
				// check probability to see if we can still spawn another TTF or need to change it to a TT.
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