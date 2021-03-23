using UnityEngine;

namespace AISystem.Critters.TerrorToothFloater.States
{
	[CreateAssetMenu(menuName = "Cuphead/Critters/MiniFlower/States/Spawn", fileName = "MiniFlower_Spawn", order = 0)]
	public class SpawnState : MiniFlowerState, IAnimatorDriveable
	{
		private bool _isAnimationDone;
		public bool IsAnimationDone => _isAnimationDone;
		public void AnimationDone()
		{
			_isAnimationDone = true;
		}

		public override void InitState()
		{
			base.InitState();

			_isAnimationDone = false;
		}

		protected override void OnStateEnter(MiniFlower fsm, Enemy enemy)
		{
			base.OnStateEnter(fsm, enemy);
			_isAnimationDone = false;
		}

		protected override MiniFlowerState OnStateUpdate(MiniFlower fsm, Enemy enemy)
		{
			if (IsAnimationDone)
			{
				return fsm.FlyToSkyState;
			}
			
			return base.OnStateUpdate(fsm, enemy);
		}
	}
}