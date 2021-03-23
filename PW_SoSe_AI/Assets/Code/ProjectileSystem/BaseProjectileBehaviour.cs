using Core;
using Core.Utility;
using UnityEngine;

namespace ProjectileSystem
{
	public abstract class BaseProjectileBehaviour : CachedMonoBehaviour
	{
		protected virtual bool OwnerIsPlayer => false;
		[SerializeField] protected float _flyingSpeed = 4f;
		[SerializeField] private int _damage = 1;
		[SerializeField] private bool _destroyOnImpact = true;

		private const float _maxLifeTime = 8f;
		private float _lifeTime = 0;
		
		protected virtual void Update()
		{
			_lifeTime += Time.deltaTime;
			if (_lifeTime > _maxLifeTime)
			{
				Destroy(gameObject);
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			IDamageable damageable = other.GetComponentInParent<IDamageable>();
			if ((damageable != null) && (damageable.IsPlayer != OwnerIsPlayer))
			{
				bool dealtDamage = damageable.OnHit(_damage);
				if (_destroyOnImpact && dealtDamage)
				{
					Destroy(gameObject);
				}
			}
		}
	}
}