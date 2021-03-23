using AISystem.CagneyCarnation;
using Core.Utility;
using UnityEngine;

namespace AISystem.Critters.TerrorTooth
{
	public class VenusFly : CachedMonoBehaviour
	{
		[SerializeField] private float _moveSpeed;
		[SerializeField] private float _delay = 0.15f;
		
		private Transform _target;
		private Enemy _enemy;
		private float _spawnTime;
		private Animator _animator;
		private bool NeedsRotation => (_target.position - transform.position).sqrMagnitude > 1.25f;
		
		protected override void Awake()
		{
			base.Awake();

			_enemy = GetComponent<Enemy>();
			_animator = GetComponent<Animator>();
			_target = FindObjectOfType<CharacterSystem.CharacterController>().TransformCached;
			_animator.SetTrigger("Bite");
			_spawnTime = Time.time;
		}

		private void Start()
		{
			EnemyPhaseFSM.OnPhaseSwitched += OnPhaseSwitch;
		}

		private void OnDestroy()
		{
			EnemyPhaseFSM.OnPhaseSwitched -= OnPhaseSwitch;
		}

		private void OnPhaseSwitch(int obj)
		{
			if (obj >= 3)
			{
				_enemy.Kill();
			}
		}

		private void Update()
		{
			if (_enemy.IsDead)
			{
				return;
			}
			
			if (_spawnTime + _delay > Time.time)
			{
				return;
			}
			
			transform.position += transform.right * (-1 * _moveSpeed * Time.deltaTime);
			if (NeedsRotation)
				transform.rotation = Quaternion.LookRotation((_target.position - transform.position).normalized, transform.up) * Quaternion.Euler(new Vector3(0, 90, 0));
			// transform.rotation = transform.rotation * Quaternion.Euler(0f, 0f, _turnSpeed * Time.deltaTime);
		}
	}
}