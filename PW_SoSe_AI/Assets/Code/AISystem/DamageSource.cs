using Core;
using Core.Utility;
using UnityEngine;

namespace AISystem
{
	public class DamageSource : CachedMonoBehaviour
	{
		private IDamageable _damageable;

		private void OnTriggerEnter2D(Collider2D other)
		{
			_damageable = other.GetComponentInParent<IDamageable>();
		}

		private void OnTriggerStay2D(Collider2D other)
		{
			if ((_damageable != null) && (_damageable.IsPlayer && !_damageable.IsInvincible))
			{
				_damageable.OnHit(1);
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			_damageable = null;
		}
	}
}