using UnityEngine;

namespace Core.Utility
{
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