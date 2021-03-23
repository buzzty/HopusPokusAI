using Core.Utility;
using UnityEngine;

namespace CharacterSystem
{
	public class CharacterFeet : CachedMonoBehaviour
	{
		[SerializeField] private LayerMask _groundLayerMask = default;
		[SerializeField] private float _radius = 0.25f;
		
		public bool IsGrounded => _isGrounded;
		public bool Landed => _isGrounded && !_wasGroundedLastFrame;
		private bool _isGrounded = false;
		private bool _wasGroundedLastFrame;
		private RaycastHit2D _raycastHit2D;

		public void Tick()
		{
			_wasGroundedLastFrame = _isGrounded;
			_raycastHit2D = Physics2D.CircleCast(transform.position, _radius, Vector2.zero, _radius, _groundLayerMask);

			_isGrounded = _raycastHit2D;
		}

		public float GroundCorrection()
		{
			if (!_raycastHit2D)
			{
				return 0;
			}
			
			float groundYPosWS = (_raycastHit2D.transform.position.y + (_radius * 0.9f)) + _raycastHit2D.collider.bounds.extents.y;
			return groundYPosWS - TransformCached.position.y;
		}
		
#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.gray;
			Gizmos.DrawWireSphere(transform.position, _radius);
		}
#endif
	}
}