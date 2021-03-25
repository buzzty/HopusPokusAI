using AISystem.CagneyCarnation;
using Core.Utility;
using UnityEngine;

namespace AISystem
{
	/// <summary>
	/// 	Base state for all attacks of CagneyCarnation. Can be driven by the animator.
	/// </summary>
	public abstract class EnemyAttackActionState : CagneyCarnationState, IAnimatorDriveable
	{
		[SerializeField] private float _attackCooldown = 3f;
		[SerializeField] private bool _limitUsageByHP = true;
		[SerializeField] [MinMaxFloat(0f, 1f)] private MinMaxFloat _hpMinMax = new MinMaxFloat(0f, 1f);
		[SerializeField] private int _chance = 1;
		public int Chance => _chance;
		public bool IsOnCooldown => _lastTimeUsed + _attackCooldown > Time.time;
		
		protected bool _isAnimationDone;
		private float _lastTimeUsed = 0;

		/// <summary>
		/// 	Is the attack currently on cooldown and not limited by current amount of hp?
		/// </summary>
		/// <param name="enemy"></param>
		/// <param name="isChainAttack"></param>
		/// <returns></returns>
		public bool CanBeUsed(Enemy enemy, bool isChainAttack)
		{
			return (!IsOnCooldown || isChainAttack) && (!_limitUsageByHP || enemy.NormalizedHealth.IsInRange(_hpMinMax));
		}

		public override void InitState()
		{
			base.InitState();
			
			_isAnimationDone = false;
			_lastTimeUsed = 0;
		}

		protected override void OnStateExit(CagneyCarnationFsm fsm, Enemy enemy)
		{
			base.OnStateExit(fsm, enemy);
			
			_lastTimeUsed = Time.time;
			fsm.FinishAttack();
		}

		protected override CagneyCarnationState OnStateUpdate(CagneyCarnationFsm fsm, Enemy enemy)
		{
			if (_isAnimationDone)
			{
				fsm.ForceExit();
				return fsm.IsChainAttack ? fsm.GetNextState(enemy) : fsm.Idle;
			}

			return base.OnStateUpdate(fsm, enemy);
		}

		protected override void OnStateEnter(CagneyCarnationFsm fsm, Enemy enemy)
		{
			base.OnStateEnter(fsm, enemy);

			_isAnimationDone = false;
			fsm.StartAttack();
		}

		public bool IsAnimationDone => _isAnimationDone;
		
		public void AnimationDone()
		{
			_isAnimationDone = true;
		}
	}
}