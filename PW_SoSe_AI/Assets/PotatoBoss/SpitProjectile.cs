using System;
using UnityEngine;

public class SpitProjectile : MonoBehaviour
{
	[SerializeField] private float _projectileSpeed;
	[SerializeField] private float _maxLifetime;

	private float _currentLifetime;

	private void Update()
	{
		// increase position by _projectileSpeed * Time.deltaTime * Vector3.left (-1, 0, 0)
		transform.position += Vector3.left * _projectileSpeed * Time.deltaTime;
		_currentLifetime += Time.deltaTime;
		// lifetime exceeds max, destroy
		if (_currentLifetime >= _maxLifetime)
		{
			Destroy(gameObject);
		}
	}
}