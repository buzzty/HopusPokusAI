using Core.Utility;
using UnityEngine;

namespace AISystem.CagneyCarnation.States
{
	public class GatlingGunSpawnPositionMarker : CachedMonoBehaviour
	{
		[SerializeField] private Transform _leftBorder = default;
		[SerializeField] private Transform _rightBorder = default;

		public float GetRandomPointWithinBorders => Random.Range(_leftBorder.position.x, _rightBorder.position.x);
	}
}