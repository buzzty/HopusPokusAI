using System.Collections.Generic;
using System.Linq;
using AISystem.CagneyCarnation.States;
using UnityEngine;

namespace AISystem.CagneyCarnation
{
	/// <summary>
	///  Concrete Implementation of <see cref="EnemyActionFSM{TState}"/> it adds a bunch of attack states and its own idle state. Also offers some variables to tweak the behaviour.
	/// </summary>
	public class CagneyCarnationFsm : EnemyActionFSM<CagneyCarnationState>
	{
		[SerializeField] protected Idle _idle = default;
		[SerializeField] private List<EnemyAttackActionState> _attackActions = new List<EnemyAttackActionState>();
		// general attack cooldown for the boss
		[SerializeField] private float _minDelayBetweenAttacks = 3.0f;
		[SerializeField] private float _maxDelayBetweenAttacks = 3.0f;
		// how many attacks to chain at max for how much hp
		[SerializeField] private AnimationCurve _attackChainAmount = default;
		
		public Idle Idle => _idle;
		public bool IsOnGlobalCooldown => !_isChainAttack && (_lastAttackTime + _attackDelay > Time.time);
		public bool IsChainAttack => _isChainAttack;
		
		private bool _isChainAttack;
		private int _attacksRemaining;
		private float _lastAttackTime;
		private float _attackDelay;
		
		protected override void Awake()
		{
			base.Awake();
			
			_currentState = _idle;
		}

		protected override void CollectAllStates()
		{
			_allStates.Add(_idle);
			_allStates.AddRange(_attackActions);
		}
		
		public CagneyCarnationState GetNextState(Enemy enemy)
		{
			float r = Random.value;
			// randomly choose a state form the attack states
			foreach (KeyValuePair<EnemyAttackActionState,float> mappingEntry in GetAttackActionProbabilityMapping(enemy))
			{
				if (r <= mappingEntry.Value)
				{
					return mappingEntry.Key;
				}

				r -= mappingEntry.Value;
			}

			return _idle;
		}

		public void StartAttack()
		{
			// already in chain attack, dont start another one
			if (_isChainAttack)
			{
				_attacksRemaining--;
				if (_attacksRemaining <= 0)
				{
					_isChainAttack = false;
				}
				return;
			}
			
			float r = Random.value;
			// start a chain attack if we rolled for more than 1 attack
			_attacksRemaining = (int) _attackChainAmount.Evaluate(r) - 1;
			_isChainAttack = _attacksRemaining > 0;
		}
		
		public void FinishAttack()
		{
			_lastAttackTime = Time.time;
			_attackDelay = Random.Range(_minDelayBetweenAttacks, _maxDelayBetweenAttacks);
		}
		
		private Dictionary<EnemyAttackActionState, float> GetAttackActionProbabilityMapping(Enemy enemy)
		{
			// attack states are weighted: the higher the weight, the more likely it is to get chosen.
			Dictionary<EnemyAttackActionState, float> mapping = new Dictionary<EnemyAttackActionState, float>();
			IEnumerable<EnemyAttackActionState> useableAttackActions = _attackActions.Where(c => c.CanBeUsed(enemy, _isChainAttack));
			
			// add all possible (not on cooldown, avialable for current hp) attacks and check how their chance correlates to current max amount
			float totalChance = useableAttackActions.Sum(c => c.Chance);
			foreach (EnemyAttackActionState enemyAttackActionState in useableAttackActions)
			{
				mapping.Add(enemyAttackActionState, enemyAttackActionState.Chance / totalChance);
			}

			return mapping;
		}
	}
}