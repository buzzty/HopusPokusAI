using UnityEngine;

namespace CharacterSystem
{
	public static class CharacterAnimParams
	{
		public static readonly int IsShooting = Animator.StringToHash("IsShooting");
		public static readonly int Jump = Animator.StringToHash("Jump");
		public static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
		public static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
		public static readonly int IsDead = Animator.StringToHash("IsDead");
		public static readonly int Hit = Animator.StringToHash("Hit");
	}
}