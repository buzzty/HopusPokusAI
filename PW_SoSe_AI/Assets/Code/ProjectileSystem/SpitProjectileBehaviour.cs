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
			// move along the x-axis, while using sin based movement along the y-axis
			transform.position = new Vector3(transform.position.x - (_flyingSpeed * Time.deltaTime), _amplitude * Mathf.Sin(Time.time * _speed), 0f);
		}
	}
}