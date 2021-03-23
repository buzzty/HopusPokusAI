using System;
using UnityEngine;

namespace Core.Utility
{
	public class Collision2DEventForwarder : CachedMonoBehaviour
	{
		public event Action<Collider2D> ForwardTriggerEnter2D;
		
		private Collider2D _collider;

		protected override void Awake()
		{
			base.Awake();
			
			_collider = GetComponent<Collider2D>();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			ForwardTriggerEnter2D?.Invoke(other);
		}
	}
}