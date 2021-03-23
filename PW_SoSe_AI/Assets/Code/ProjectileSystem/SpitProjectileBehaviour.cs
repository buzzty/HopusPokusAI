using UnityEngine;

namespace ProjectileSystem
{
	public class SpitProjectileBehaviour : BaseProjectileBehaviour
	{
		[SerializeField] private float _amplitude = 1f;
		[SerializeField] private float _speed = 1f;

		protected override void Update()
		{
			base.Update();
			transform.position = new Vector3(transform.position.x - (_flyingSpeed * Time.deltaTime), _amplitude * Mathf.Sin(Time.time * _speed), 0f);
		}
	}
}