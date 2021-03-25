using Core.Utility;
using ProjectileSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AISystem.CagneyCarnation.States
{
	[CreateAssetMenu(menuName = "Cuphead/Bosses/CagneyCarnation/Attacks/Gatling", fileName = "Gatling", order = 0)]
	public class GatlingGunAction : HPEnemyAttackActionState
	{
		[SerializeField] private SeedProjectileBehaviour _normalProjectile = default;
		[SerializeField] private SeedProjectileBehaviour _parryProjectile = default;
		[SerializeField] private float _parryProjectileProbability = 0.3f;
		[SerializeField] private float _minDistanceBetweenSeeds = 1.25f;
		[SerializeField] [MinMaxFloat(0.25f, 0.75f)]
		private MinMaxFloat _startSpawnDelay = new MinMaxFloat(0.25f, 0.75f);
		[SerializeField] [MinMaxFloat(0.75f, 1.75f)]
		private MinMaxFloat _delayBetweenSpawns = new MinMaxFloat(0.75f, 1.75f);
		[SerializeField] private int _minAmount = 2;
		[SerializeField] private int _maxAmount = 5;
		[SerializeField] private AudioClip _gatlingAudioLoop = default;
		
		private int _targetAmount;
		private int _currentAmount;
		private float _stateEnterTime;
		private float _lastSpawnTime;
		private float _spawnDelay;
		private float _betweenSpawns;
		private GatlingGunSpawnPositionMarker _spawnPoint;
		private float _lastOffsetX;

		public override void InitState()
		{
			base.InitState();

			_spawnPoint = FindObjectOfType<GatlingGunSpawnPositionMarker>();
		}

		protected override void OnStateEnter(CagneyCarnationFsm fsm, Enemy enemy)
		{
			base.OnStateEnter(fsm, enemy);
			
			enemy.Animator.SetTrigger("Gatling");
			enemy.PlayAudio(_gatlingAudioLoop, true);
			
			_lastSpawnTime = Time.time;
			_stateEnterTime = Time.time;
			_currentAmount = 0;
			_targetAmount = Random.Range(_minAmount, _maxAmount);
			_spawnDelay = _startSpawnDelay.GetRandomBetween();
			_betweenSpawns = _delayBetweenSpawns.GetRandomBetween();
		}

		protected override void OnStateExit(CagneyCarnationFsm fsm, Enemy enemy)
		{
			base.OnStateExit(fsm, enemy);
			
			enemy.StopAudio();
		}

		protected override CagneyCarnationState OnStateUpdate(CagneyCarnationFsm fsm, Enemy enemy)
		{
			bool needsInitialSpawn = (_currentAmount == 0) && (_stateEnterTime +_spawnDelay < Time.time);
			bool targetSpawnAmountReached = _currentAmount >= _targetAmount;
			bool canSpawnAgain = _lastSpawnTime + _spawnDelay < Time.time;
			if (!targetSpawnAmountReached && (needsInitialSpawn || canSpawnAgain))
			{
				SpawnProjectile(needsInitialSpawn);
			}
			
			return base.OnStateUpdate(fsm, enemy);
		}

		private void SpawnProjectile(bool first)
		{
			_lastSpawnTime = Time.time;
			_currentAmount++;
			SeedProjectileBehaviour seedProjectileBehaviour = Instantiate(Random.value < _parryProjectileProbability ? _parryProjectile : _normalProjectile, _spawnPoint.TransformCached);

			float spawnPointOffsetX;
			if (first)
			{
				spawnPointOffsetX = _spawnPoint.GetRandomPointWithinBorders;
			}
			else
			{
				int tries = 0;
				do
				{
					spawnPointOffsetX = _spawnPoint.GetRandomPointWithinBorders;
					tries++;
				} while ((Mathf.Abs(spawnPointOffsetX - _lastOffsetX) < _minDistanceBetweenSeeds) && (tries < 1000));
			}

			seedProjectileBehaviour.transform.position = new Vector3(spawnPointOffsetX, seedProjectileBehaviour.transform.position.y, 0);
			_lastOffsetX = spawnPointOffsetX;
		}
	}
}