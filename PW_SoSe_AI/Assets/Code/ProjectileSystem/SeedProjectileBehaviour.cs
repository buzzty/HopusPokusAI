using System;
using AISystem.Critters;
using UnityEngine;

namespace ProjectileSystem
{
	public class SeedProjectileBehaviour : BaseProjectileBehaviour
	{
		/// <summary>
		/// 	Event sending information on what type of seed hit the ground and where.
		/// </summary>
		public static event Action<SeedType, Transform> OnHitGround;

		[SerializeField] private SeedType _seedType;
		
		private Animator _animator;
		private bool _isBurrowing;

		protected override void Awake()
		{
			base.Awake();
			
			_animator = GetComponent<Animator>();
		}

		protected override void Update()
		{
			base.Update();
			if (_isBurrowing)
			{
				return;
			}
			
			// falling down using fake gravity - not actual physics gravity
			transform.position += Physics.gravity * (_flyingSpeed * Time.deltaTime);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			// if we hit the ground (not the character), burrow
			if (!other.GetComponent<CharacterSystem.CharacterController>())
			{
				// play animation
				_animator.SetTrigger("Burrow");
				_isBurrowing = true;

				OnHitGround?.Invoke(_seedType, transform);
				Destroy(gameObject, 0.25f);
			}
		}
	}
}