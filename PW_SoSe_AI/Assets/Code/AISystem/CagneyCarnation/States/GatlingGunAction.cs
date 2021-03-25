using Core.Utility;
using ProjectileSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AISystem.CagneyCarnation.States
{
	[CreateAssetMenu(menuName = "Cuphead/Bosses/CagneyCarnation/Attacks/Gatling", fileName = "Gatling", order = 0)]
	public class GatlingGunAction : EnemyAttackActionState
	{
		// can spawn normal and parryable projectile
		[SerializeField] private SeedProjectileBehaviour _normalProjectile = default;
		[SerializeField] private SeedProjectileBehaviour _parryProjectile = default;
		// probability to spawn pink, parryable projectile
		[SerializeField] private float _parryProjectileProbability = 0.3f;
		// how far from another they should be apart
		[SerializeField] private float _minDistanceBetweenSeeds = 1.25f;
		// delay of when to start spawning projectiles when anim starts
		[SerializeField] [MinMaxFloat(0.25f, 0.75f)]
		private MinMaxFloat _startSpawnDelay = new MinMaxFloat(0.25f, 0.75f);
		// delay between projectiles, as a range of time
		[SerializeField] [MinMaxFloat(0.75f, 1.75f)]
		private MinMaxFloat _delayBetweenSpawns = new MinMaxFloat(0.75f, 1.75f);
		// min max amount of projectiles to spawn
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
			
			// playback audio
			enemy.Animator.SetTrigger("Gatling");
			enemy.PlayAudio(_gatlingAudioLoop, true);
			
			// reset params and initialize them 
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
			// do we need to spawn one (cooldown between shots as passed) ?
			bool needsInitialSpawn = (_currentAmount == 0) && (_stateEnterTime +_spawnDelay < Time.time);
			bool targetSpawnAmountReached = _currentAmount >= _targetAmount;
			bool canSpawnAgain = _lastSpawnTime + _spawnDelay < Time.time;
			// spawn
			if (!targetSpawnAmountReached && (needsInitialSpawn || canSpawnAgain))
			{
				SpawnProjectile(needsInitialSpawn);
			}
			
			// check for next state (driven by parent state)
			return base.OnStateUpdate(fsm, enemy);
		}

		private void SpawnProjectile(bool first)
		{
			_lastSpawnTime = Time.time;
			// keep track of projectiles spawned
			_currentAmount++;
			// normal/parry projectile?
			SeedProjectileBehaviour seedProjectileBehaviour = Instantiate(Random.value < _parryProjectileProbability ? _parryProjectile : _normalProjectile, _spawnPoint.TransformCached);

			// handle offset between projectiles
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
					// must be atleast _minDistance units away - try at max 1000 times, then use whatever we ended up with
				} while ((Mathf.Abs(spawnPointOffsetX - _lastOffsetX) < _minDistanceBetweenSeeds) && (tries < 1000));
			}

			seedProjectileBehaviour.transform.position = new Vector3(spawnPointOffsetX, seedProjectileBehaviour.transform.position.y, 0);
			_lastOffsetX = spawnPointOffsetX;
		}
	}
}