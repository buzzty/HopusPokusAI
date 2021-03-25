using System;
using Core.Utility;
using UnityEngine;

namespace CharacterSystem
{
	public class CharacterAim : CachedMonoBehaviour
	{
		private Camera _camera;

		private void Start()
		{
			_camera = Camera.main;
		}

		public void Tick()
		{
			Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
			mousePosition.z = 0f;
			transform.rotation = Quaternion.LookRotation(TransformCached.position - mousePosition, Vector3.up) * Quaternion.Euler(0f, 90f, 0f);
		}
	}
}