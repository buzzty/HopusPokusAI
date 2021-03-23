using Core.Utility;
using UnityEngine;

namespace AISystem.Critters.TerrorToothFloater
{
	public class MiniFlowerHeightMarker : CachedMonoBehaviour
	{
		[SerializeField] private Transform _left = default;
		[SerializeField] private Transform _right = default;

		public bool HitBorder(Vector3 pos) => ((_left.position - pos).sqrMagnitude < 1f) || ((_right.position - pos).sqrMagnitude < 1f);
	}
}