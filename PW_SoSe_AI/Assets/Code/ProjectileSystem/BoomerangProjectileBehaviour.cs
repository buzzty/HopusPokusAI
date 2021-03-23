using Core.Utility;
using UnityEngine;

namespace ProjectileSystem
{
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
			transform.position += (_toLeft ? Vector3.left : Vector3.right) * (_flyingSpeed * Time.deltaTime);
		}

		public void TurnAround()
		{
			_toLeft = !_toLeft;
		}
	}
}