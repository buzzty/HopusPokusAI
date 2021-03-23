using System;
using AISystem;
using Core;
using Core.Utility;
using ProjectileSystem;
using UnityEngine;

namespace CharacterSystem
{
	public class PlayerProjectile : BaseProjectileBehaviour
	{
		protected override bool OwnerIsPlayer => true;

		protected override void Update()
		{
			base.Update();
			TransformCached.Translate(Vector3.right * (_flyingSpeed * Time.deltaTime));
		}
	}
}