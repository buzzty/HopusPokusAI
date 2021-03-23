using System;
using System.Collections;
using Core.Utility;
using UnityEngine;

namespace AISystem.Vines
{
	public class VinePlatform : CachedMonoBehaviour
	{
		[SerializeField] [MinMaxFloat(0.25f, 2f)]
		private MinMaxFloat _minMaxDelayUntilPierce = new MinMaxFloat(0.25f, 1.25f);

		public bool IsBusy => _state != VinePlatformState.Idle;

		private VinePlatformState _state = VinePlatformState.Idle;
		private Animator _animator;
		private float _spawnTime;
		private float _pierceTime;
		private float _spawnDelay;

		protected override void Awake()
		{
			base.Awake();
			
			_animator = GetComponent<Animator>();
			gameObject.SetActive(false);
		}

		private void Update()
		{
			switch (_state)
			{
				case VinePlatformState.Idle:
					break;
				case VinePlatformState.Spawn:
					if (Time.time > _spawnTime)
					{
						Spawn();
					}
					break;
				case VinePlatformState.WaitForLoop:
					break;
				case VinePlatformState.Loop:
					if (Time.time > _pierceTime)
					{
						Pierce();
					}
					break;
				case VinePlatformState.Pierce:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void Pierce()
		{
			_state = VinePlatformState.Pierce;
			_animator.SetTrigger("Pierce");
		}

		public void ScheduleForSpawn(float delay)
		{
			_state = VinePlatformState.Spawn;
			_spawnTime = Time.time + delay;
			_spawnDelay = delay;
			gameObject.SetActive(true);
		}

		private void Spawn()
		{
			_animator.SetTrigger("Grow");
			_state = VinePlatformState.WaitForLoop;
		}
		
		private void Hide()
		{
			StartCoroutine(HideInternal());
		}

		private IEnumerator HideInternal()
		{
			yield return new WaitForSeconds(0.75f);
			gameObject.SetActive(false);
			_state = VinePlatformState.Idle;
		}

		private void SpawnDone()
		{
			_state = VinePlatformState.Loop;
			_pierceTime = Time.time + _minMaxDelayUntilPierce.GetRandomBetween() + _spawnDelay;
		}
	}
}