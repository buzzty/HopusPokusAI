using UnityEngine;

namespace ProjectileSystem
{
	public class MiniFlowerSpitProjectile : BaseProjectileBehaviour
	{
		private CharacterSystem.CharacterController _player;
		
		protected override void Awake()
		{
			base.Awake();

			_player = FindObjectOfType<CharacterSystem.CharacterController>();
			LockToTarget();
		}

		protected override void Update()
		{
			base.Update();
			// fly in a straight line
			TransformCached.position += TransformCached.right * (_flyingSpeed * Time.deltaTime);
		}

		private void LockToTarget()
		{
			// calculate directional vector between spawn point and player
			Vector3 dir = transform.position - _player.transform.position;
			// rotate towards the player
			transform.rotation = Quaternion.LookRotation(dir.normalized, transform.up) * Quaternion.Euler(new Vector3(0, 90, 0));
		}
	}
}