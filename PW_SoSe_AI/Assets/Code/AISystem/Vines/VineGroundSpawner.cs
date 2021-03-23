using System.Collections.Generic;
using AISystem.CagneyCarnation;
using Core.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AISystem.Vines
{
	public class VineGroundSpawner : CachedMonoBehaviour
	{
		[SerializeField] [MinMaxFloat(0.5f, 7.5f)] private MinMaxFloat _minMaxDelayBetweenGroupSpawns = new MinMaxFloat(4f, 7.5f);
		[SerializeField] private float _delayBetweenSpawns = 0.25f;
		[SerializeField] private float _spawnGroupProbability = 0.35f;
		[SerializeField] private int _maxBusyPlatforms = 2;

		private float _lastTimeSpawned;
		private float _waitTimeUntilNextSpawn;
		private int _allPlatformsCount;
		private VinePlatform[] _vinePlatforms;
		
		protected override void Awake()
		{
			base.Awake();

			_vinePlatforms = GetComponentsInChildren<VinePlatform>(true);
			_allPlatformsCount = _vinePlatforms.Length;
		}

		private void Start()
		{
			EnemyPhaseFSM.OnPhaseSwitched += OnPhaseSwitch;
			gameObject.SetActive(false);
		}

		private void OnDestroy()
		{
			EnemyPhaseFSM.OnPhaseSwitched -= OnPhaseSwitch;
		}

		private void Update()
		{
			if (_lastTimeSpawned + _waitTimeUntilNextSpawn < Time.time)
			{
				TrySpawnVines(Random.value < _spawnGroupProbability ? 2 : 1);
			}
		}

		private void OnPhaseSwitch(int phaseIndex)
		{
			if (phaseIndex == 3)
			{
				StartCooldown();
				gameObject.SetActive(true);
			}
		}

		private void TrySpawnVines(int amount)
		{
			int busyCount = 0;
			List<VinePlatform> availablePlatforms = new List<VinePlatform>();
			foreach (VinePlatform vinePlatform in _vinePlatforms)
			{
				if (vinePlatform.IsBusy)
				{
					busyCount++;
					if (busyCount >= _maxBusyPlatforms)
					{
						// fake cooldown of 1 second se we dont "spam" try spawn
						_lastTimeSpawned = Time.time;
						_waitTimeUntilNextSpawn = 1.0f;
						return;
					}
				}
				else
				{
					availablePlatforms.Add(vinePlatform);
				}
			}

			amount = Mathf.Min(amount, _allPlatformsCount - busyCount);
			availablePlatforms.Shuffle();
			for (int i = 0; i < Mathf.Min(availablePlatforms.Count, amount); i++)
			{
				availablePlatforms[i].ScheduleForSpawn(_delayBetweenSpawns * i);
			}

			StartCooldown();
		}

		private void StartCooldown()
		{
			_lastTimeSpawned = Time.time;
			_waitTimeUntilNextSpawn = _minMaxDelayBetweenGroupSpawns.GetRandomBetween();
		}
	}
}