using System;
using Core;
using Core.Utility;
using UnityEngine;

namespace AISystem
{
	/// <summary>
	/// 	Provides basic ocmponents for the enemy (audio, animator, ...) and implements the <see cref="IDamageable"/> interface. 
	/// </summary>
	public class Enemy : CachedMonoBehaviour, IDamageable
	{
		public static event Action BossDeath;
		public static event Action<Enemy> EnemyDeath;
		
		[SerializeField] private float _maxHP = 100f;
		[SerializeField] private bool _isBoss = false;
		[SerializeField] private SpriteRenderer _spriteRenderer = default;
		[SerializeField] private float _onHitLerpEffectDuration = 0.25f;

		private Collider2D _collider;
		[SerializeField] private AudioSource _audio;
		private Animator _animator;
		private HPEnemyBrain _brain;
		private float _currentHP;
		private Color _originalColor;
		private Color _hitColor;
		private bool _gotHit;
		private float _hitLerpDuration;
		private bool _isDead;
		public bool IsInvincible
		{
			get;
			set;
		}

		public float NormalizedHealth => _currentHP / _maxHP;
		public Animator Animator => _animator;
		public bool IsPlayer => false;
		public bool IsDead => _currentHP <= 0;
		
		protected override void Awake()
		{
			base.Awake();
			
			_animator = GetComponentInChildren<Animator>();
			_audio = GetComponentInChildren<AudioSource>();
			_collider = GetComponentInChildren<Collider2D>();
			_originalColor = _spriteRenderer.color;
			_hitColor = new Color(_originalColor.r, _originalColor.g, _originalColor.b, 0.25f);
			_currentHP = _maxHP;
		}

		private void Update()
		{
			// Debug only TODO remove
			if (Input.GetKeyDown(KeyCode.K))
			{
				Die();
			}

			if (_gotHit)
			{
				_spriteRenderer.color = Color.Lerp(_hitColor, _originalColor, _hitLerpDuration / _onHitLerpEffectDuration);
				_hitLerpDuration += Time.deltaTime;
				if (_spriteRenderer.color.Equals(_originalColor))
				{
					_gotHit = false;
				}
			}
		}

		public bool OnHit(int damage)
		{
			if (IsInvincible)
			{
				return false;
			}
			
			_currentHP -= damage;
			_spriteRenderer.color = _hitColor;
			_hitLerpDuration = 0f;
			_gotHit = true;
			
			if (_currentHP <= 0)
			{
				Die();
			}

			return true;
		}

		private void Die()
		{
			if (_isBoss)
			{
				BossDeath?.Invoke();
			}
			else
			{
				_collider.enabled = false;
				_animator.SetTrigger("Die");
				EnemyDeath?.Invoke(this);
				Destroy(gameObject, 0.5f);
			}
		}

		public void PlayAudio(AudioClip clip, bool loop)
		{
			_audio.clip = clip;
			_audio.loop = loop;
			_audio.Play();
		}

		public void StopAudio()
		{
			_audio.Stop();
		}

		public void Kill()
		{
			OnHit((int) _maxHP);
		}
	}
}