using System;
using Core;
using Core.Utility;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterSystem
{
	public class PlayerDamageable : CachedMonoBehaviour, IDamageable
	{
		public static event Action PlayerDeath;
		public static event Action PlayerHit;
		
		[SerializeField] private int _maxHP = 3;
		[SerializeField] private float _invincibilityDuration = 1.5f;
		[SerializeField] private SpriteRenderer _playerGraphicsObject = default;
		[SerializeField] private PlayerHPUI _hpUserInterface = default;
		[SerializeField] private float _moveUpOnDeathHeight = 2f;
		[SerializeField] private AudioSource _audioSource;
		[SerializeField] private float _moveUpOnDeathSpeed;
		
		public bool IsInvincible { get => _isInvincible; set => _isInvincible = value; }
		private int _currentHP;
		private float _currentInvincibleTime;
		private Vector3 _deathTargetPosition;
		private bool _isInvincible;
		private bool _isDead;
		private Vector3 _diedAtPosition;
		private float _t;

		protected override void Awake()
		{
			base.Awake();

			_currentHP = _maxHP;
			_hpUserInterface.InitForHP(_currentHP);

			PlayerDeath = null;
			PlayerHit = null;
		}

		private void Update()
		{
			if (_isDead)
			{
				_t += Time.deltaTime;
				transform.position = Vector3.Lerp(_diedAtPosition, _deathTargetPosition, _t / _moveUpOnDeathSpeed);
				return;
			}
			// TODO remove after testing
			if (Input.GetKeyDown(KeyCode.I))
			{
				OnHit(1);
			}
			
			if (Input.GetKeyDown(KeyCode.L))
			{
				Die();
			}
			
			if (!_isInvincible)
			{
				return;
			}

			if (Time.frameCount % 16 == 0)
			{
				_playerGraphicsObject.enabled = !_playerGraphicsObject.enabled;
			}
			
			_currentInvincibleTime += Time.deltaTime;
			if (_currentInvincibleTime >= _invincibilityDuration)
			{
				_isInvincible = false;
				_playerGraphicsObject.enabled = true;
			}
		}

		public bool IsPlayer => true;

		public bool OnHit(int damage)
		{
			if (IsInvincible)
			{
				return false;
			}
			
			_audioSource.Play();
			_currentHP--;
			_hpUserInterface.UpdateUI(_currentHP);

			if (_currentHP <= 0)
			{
				Die();
			}
			else
			{
				SetInvincible();
				PlayerHit?.Invoke();
			}

			return true;
		}

		private void SetInvincible()
		{
			_isInvincible = true;
			_currentInvincibleTime = 0;
			_playerGraphicsObject.enabled = false;
		}

		private void Die()
		{
			_isDead = true;
			_diedAtPosition = transform.position;
			_deathTargetPosition = _diedAtPosition + (Vector3.up * _moveUpOnDeathHeight);
			PlayerDeath?.Invoke();
		}
	}
}