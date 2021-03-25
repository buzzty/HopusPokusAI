using Core.Utility;
using UnityEngine;

namespace ProjectileSystem
{
	/// <summary>
	/// 	Class for the boomerang used by Cagney Carnation. Implements fly left then right behaviour
	/// </summary>
	public class BoomerangProjectileBehaviour : BaseProjectileBehaviour
	{
		[SerializeField] [MinMaxFloat(0.5f, 2f)]
		private MinMaxFloat _minMaxStartFlyingDelay = new MinMaxFloat(0.5f, 2f);

		private float _currentLiveTime;
		private float _startFlyingDelay;
		private bool _toLeft = true;
		
		protected override void Awake()
		{
			base.Awake();

			_startFlyingDelay = _minMaxStartFlyingDelay.GetRandomBetween();
		}

		protected override void Update()
		{
			base.Update();
			_currentLiveTime += Time.deltaTime;
			if (_currentLiveTime > _startFlyingDelay)
			{
				Fly();
			}
		}

		private void Fly()
		{
			// if we are supposed to fly to the left, use vector3.Left, else right. Slightly simpler form of a regular if else statement
			// same as:
			// if (_toLeft) transform.position += Vector3.left * _flyingSpeed * Time.deltaTime;
			// else transform.position += Vector3.right * _flyingSpeed * Time.deltaTime;
			transform.position += (_toLeft ? Vector3.left : Vector3.right) * (_flyingSpeed * Time.deltaTime);
		}

		/// <summary>
		/// 	Flip current movement direction
		/// </summary>
		public void TurnAround()
		{
			_toLeft = !_toLeft;
		}
	}
}