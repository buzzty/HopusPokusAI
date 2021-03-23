using System;
using Core;
using UnityEngine;

namespace InputSystem
{
	public class InputManager : MonoSingleton<InputManager>
	{
		private const string ShootButtonName = "Fire1";
		private const string JumpButtonName = "Jump";
		private const string HorizontalAxisName = "Horizontal";
		
		public bool Shoot => Input.GetButton(ShootButtonName);
		public bool ShootUp => Input.GetButtonUp(ShootButtonName);
		public bool JumpDown => Input.GetButtonDown(JumpButtonName);
		public float HorizontalInputRaw => Input.GetAxisRaw(HorizontalAxisName);
	}
}