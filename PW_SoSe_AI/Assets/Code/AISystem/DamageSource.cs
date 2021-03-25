using Core;
using Core.Utility;
using UnityEngine;

namespace AISystem
{
	/// <summary>
	/// 	When in contact with the player, damages the player
	/// </summary>
	public class DamageSource : CachedMonoBehaviour
	{
		private IDamageable _damageable;

		private void OnTriggerEnter2D(Collider2D other)
		{
			// player entered
			_damageable = other.GetComponentInParent<IDamageable>();
		}

		private void OnTriggerStay2D(Collider2D other)
		{
			// player still in, not invincible anymore - need to damage it again!
			if ((_damageable != null) && (_damageable.IsPlayer && !_damageable.IsInvincible))
			{
				_damageable.OnHit(1);
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			// player exitted, no more damageable in there
			_damageable = null;
		}
	}
}