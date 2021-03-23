using Core.Utility;
using ProjectileSystem;
using UnityEngine;

namespace AISystem
{
	public class BoomerangZone : CachedMonoBehaviour
	{
		[SerializeField] private Transform _targetPoint = default;
		[SerializeField] private bool _destroyBoomerang = false;

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
				boomerangProjectileBehaviour.transform.position = _targetPoint.position;
				boomerangProjectileBehaviour.TurnAround();
			}
		}
	}
}