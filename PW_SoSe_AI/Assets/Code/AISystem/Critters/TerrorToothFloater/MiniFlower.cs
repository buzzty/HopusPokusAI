using AISystem.Critters.TerrorToothFloater.States;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AISystem.Critters.TerrorToothFloater
{
	public class MiniFlower : EnemyActionFSM<MiniFlowerState>
	{
		[SerializeField] private Enemy _enemy;
		[SerializeField] private SpawnState _spawnState = default;
		[SerializeField] private FlyToSkyState _flyToSkyState = default;
		[SerializeField] private FlapState _flapState = default;
		[SerializeField] private ShootState _shootState = default;
		[SerializeField] private GameObject _deathParticles = default;
		
		public FlapState FlapState => _flapState;
		public ShootState ShootState => _shootState;
		public FlyToSkyState FlyToSkyState => _flyToSkyState;
		public bool ToLeft { get; set; }
		public float FlapDuration { get; set; }
		
		protected override void Awake()
		{
			base.Awake();

			_currentState = _spawnState;
			ToLeft = Random.value > 0.5f;

			_deathParticles.SetActive(false);
			_enemy.IsInvincible = true;
		}
		
		private void Start()
		{
			Enemy.EnemyDeath += OnEnemyDeath;
		}

		private void OnEnemyDeath(Enemy obj)
		{
			if (obj.Equals(_enemy))
			{
				_deathParticles.SetActive(true);
			}
		}

		private void Update()
		{
			if (_enemy.IsDead)
			{
				return;
			}
			
			Tick(_enemy);
		}

		protected override void CollectAllStates()
		{
			_allStates.Add(_spawnState);
			_allStates.Add(_flyToSkyState);
			_allStates.Add(_flapState);
			_allStates.Add(_shootState);
		}
	}
}