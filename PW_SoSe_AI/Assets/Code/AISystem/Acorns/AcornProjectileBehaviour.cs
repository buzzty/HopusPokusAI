using System;
using ProjectileSystem;
using UnityEngine;

namespace AISystem.Acorns
{
	public class AcornProjectileBehaviour : BaseProjectileBehaviour
	{
		private enum AcornState
		{
			Idle,
			Rotating,
			Flying,
		}
		
		private CharacterSystem.CharacterController _player;
		private Animator _animator;
		private AcornState _state;
		private float _rotateDuration;
		public float StartFlyingDelay { get; set; }
		
		protected override void Awake()
		{
			base.Awake();

			_animator = GetComponent<Animator>();
			_player = FindObjectOfType<CharacterSystem.CharacterController>();
			SwitchState(AcornState.Idle, AcornState.Rotating);
		}

		protected override void Update()
		{
			base.Update();
			switch (_state)
			{
				case AcornState.Rotating:
					Rotate();
					break;
				case AcornState.Flying:
					Fly();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void Rotate()
		{
			_rotateDuration += Time.deltaTime;
			if (_rotateDuration > StartFlyingDelay)
			{
				SwitchState(_state, AcornState.Flying);
			}
		}

		private void Fly()
		{
			TransformCached.position += TransformCached.right * (_flyingSpeed * Time.deltaTime);
		}

		private void SwitchState(AcornState from, AcornState to)
		{
			switch (from)
			{
				case AcornState.Rotating:
					break;
				case AcornState.Flying:
					break;
				case AcornState.Idle:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(@from), @from, null);
			}

			switch (to)
			{
				case AcornState.Rotating:
					_rotateDuration = 0f;
					_animator.SetTrigger("Rotate");
					break;
				case AcornState.Flying:
					_animator.SetTrigger("Fly");
					LockToTarget();
					break;
				case AcornState.Idle:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(to), to, null);
			}

			_state = to;
		}

		private void LockToTarget()
		{
			Vector3 dir = transform.position - _player.transform.position;
			transform.rotation = Quaternion.LookRotation(dir.normalized, transform.up) * Quaternion.Euler(new Vector3(0, 90, 0));
		}
	}
}