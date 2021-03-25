using Core.Utility;
using ProjectileSystem;
using UnityEngine;

namespace AISystem
{
	public class BoomerangZone : CachedMonoBehaviour
	{
		[SerializeField] private Transform _targetPoint = default;
		[SerializeField] private bool _destroyBoomerang = false;

		/// <summary>
		/// 	When the boomerang enters this, we either set it to the next zone or destroy it
		/// </summary>
		/// <param name="other"></param>
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (_destroyBoomerang)
			{
				Destroy(other.gameObject);
				return;
			}
			
			BoomerangProjectileBehaviour boomerangProjectileBehaviour = other.gameObject.GetComponent<BoomerangProjectileBehaviour>();
			if (boomerangProjectileBehaviour)
			{
				// update position, flip flying direction
				boomerangProjectileBehaviour.transform.position = _targetPoint.position;
				boomerangProjectileBehaviour.TurnAround();
			}
		}
	}
}