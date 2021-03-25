using Core;
using Core.Utility;
using UnityEngine;

namespace ProjectileSystem
{
	/// <summary>
	/// 	Base class for a projectile. It implements behaviour like lifetime and manages the projectiles to destroy them after a certain amount of time.
	/// 	Implementing a concrete projectile can be done by inheriting from this class.
	/// </summary>
	public abstract class BaseProjectileBehaviour : CachedMonoBehaviour
	{
		// only set this to true in the PlayerProjectile class
		protected virtual bool OwnerIsPlayer => false;
		[SerializeField] protected float _flyingSpeed = 4f;
		[SerializeField] private int _damage = 1;
		[SerializeField] private bool _destroyOnImpact = true;

		// amount of seconds a projectile stays alive
		private const float _maxLifeTime = 8f;
		// current lifetime
		private float _lifeTime = 0;
		
		protected virtual void Update()
		{
			// increase current lifetime each frame
			_lifeTime += Time.deltaTime;
			// if current lifetime exceeds max lifetime, kill it
			if (_lifeTime > _maxLifeTime)
			{
				Destroy(gameObject);
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			// check the collision of the projectile with a Damageable component
			// a damagaeable can receive dmg, so if there is a dmg somewhere on the collision object, we should dmg it
			IDamageable damageable = other.GetComponentInParent<IDamageable>();
			// player cant dmg itself and enemies cant dmg themselves either
			if ((damageable != null) && (damageable.IsPlayer != OwnerIsPlayer))
			{
				// apply _damage amount of damage
				bool dealtDamage = damageable.OnHit(_damage);
				// not all projectiles should be destroyed upon impact (e.g. boomerang)
				if (_destroyOnImpact && dealtDamage)
				{
					Destroy(gameObject);
				}
			}
		}
	}
}