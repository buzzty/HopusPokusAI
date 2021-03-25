using UnityEngine;

namespace Core.Utility
{
	/// <summary>
	/// 	Caches the transform of a mono behaviour to reduce load.
	/// 	Usually calling transform accesses it using GetComponent, which is slow.
	/// </summary>
	public class CachedMonoBehaviour : MonoBehaviour
	{
		private Transform _transformCached;
		public Transform TransformCached => _transformCached;

		protected virtual void Awake()
		{
			_transformCached = transform;
		}
	}
}