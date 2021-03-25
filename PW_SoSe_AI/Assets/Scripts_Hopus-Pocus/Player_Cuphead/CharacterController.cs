using System;
using Core.Utility;
using GameFlowSystem;
using InputSystem;
using UI;
using UnityEngine;

namespace CharacterSystem
{
	public class CharacterController : CachedMonoBehaviour
	{
		[SerializeField] private float _movementSpeed = 10f;
		[SerializeField] private float _jumpHeight = 4f;
		[SerializeField] private float _fallSpeed = 2f;
		[SerializeField] private CharacterFeet _feet = default;
		[SerializeField] private CharacterAim _aim = default;
		[SerializeField] private PlayerProjectile _playerProjectilePrefab;

		[SerializeField] private float _fireRate = 15f;

		private float _currentVerticalVelocity = 0f;
		private float JumpVelocity => Mathf.Sqrt(_jumpHeight * -2 * Physics2D.gravity.y);
		[SerializeField] private float _gravityModifier = 1.0f;

		private bool _isFacingRight = true;
		[SerializeField] private Transform _projectileSpawnPoint;

		private AudioSource _source;
		private Animator _animator;
		private float _nextFireTime;

		protected override void Awake()
		{
			base.Awake();
			
			_nextFireTime = 0f;
			_source = GetComponentInChildren<AudioSource>();
			_animator = GetComponentInChildren<Animator>();
		}

		private void Start()
		{
			PlayerDamageable.PlayerDeath += OnPlayerDeath;
			PlayerDamageable.PlayerHit += OnPlayerHit;
		}

		private void OnDestroy()
		{
			PlayerDamageable.PlayerDeath -= OnPlayerDeath;
			PlayerDamageable.PlayerHit -= OnPlayerHit;
		}

		private void OnPlayerHit()
		{
			_animator.SetTrigger(CharacterAnimParams.Hit);
		}

		private void OnPlayerDeath()
		{
			_animator.SetBool(CharacterAnimParams.IsDead, true);
		}

		private void Update()
		{
			// Player has no control is we are not in the match yet
			//if (!GameManager.Instance.IsMatch)
			//{
			//	return;
			//}
			
			// Poll Input
			float horizontalAxisRawInput = InputManager.Instance.HorizontalInputRaw;
			float deltaX = horizontalAxisRawInput * Time.deltaTime *  _movementSpeed;
			
			if (InputManager.Instance.Shoot && (_nextFireTime < Time.time))
			{
				_animator.SetBool(CharacterAnimParams.IsShooting, true);
				if (!_source.isPlaying)
				{
					_source.Play();
				}
				Instantiate(_playerProjectilePrefab, _projectileSpawnPoint.position, _projectileSpawnPoint.rotation);
				_nextFireTime = Time.time + (1 / _fireRate);
			}

			if (InputManager.Instance.ShootUp)
			{
				_animator.SetBool(CharacterAnimParams.IsShooting, false);
				if (_source.isPlaying)
				{
					_source.Stop();
				}
			}

			if (_currentVerticalVelocity < 0)
			{
				_currentVerticalVelocity += Physics2D.gravity.y * _fallSpeed * Time.deltaTime * Time.deltaTime;
			}
			else
			{
				_currentVerticalVelocity += Physics2D.gravity.y * _gravityModifier * Time.deltaTime * Time.deltaTime;
			}

			// Check Gravity
			if (_feet.IsGrounded)
			{
				_currentVerticalVelocity = 0f;
				
				if (InputManager.Instance.JumpDown)
				{
					_animator.SetTrigger(CharacterAnimParams.Jump);
					_animator.SetBool(CharacterAnimParams.IsGrounded, false);
					_currentVerticalVelocity = JumpVelocity * Time.deltaTime;
				}
			}

			Vector2 velocity = new Vector2(deltaX, _currentVerticalVelocity);
			_animator.SetFloat(CharacterAnimParams.MoveSpeed, Mathf.Abs(horizontalAxisRawInput));
			// Move
			TransformCached.Translate(velocity);
			
			// Update own components
			_feet.Tick();
			_aim.Tick();

			if (_feet.Landed)
			{
				TransformCached.Translate(0f, _feet.GroundCorrection(), 0f);
				_animator.SetBool(CharacterAnimParams.IsGrounded, true);
			}
			
			// Check Look Direction
			float lookDir = Mathf.Sign(deltaX);
			if ((deltaX != 0) && ((_isFacingRight && (lookDir < 0)) || (!_isFacingRight && (lookDir > 0))))
			{
				Flip(lookDir);
			}
		}

		private void Flip(float lookDir)
		{
			_isFacingRight = !_isFacingRight;
			TransformCached.localScale = new Vector3(lookDir, 1f, 1f);
		}
	}
}