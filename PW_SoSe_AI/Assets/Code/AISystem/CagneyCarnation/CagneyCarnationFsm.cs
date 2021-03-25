using System.Collections.Generic;
using System.Linq;
using AISystem.CagneyCarnation.States;
using UnityEngine;

namespace AISystem.CagneyCarnation
{
	public class CagneyCarnationFsm : EnemyActionFSM<CagneyCarnationState>
	{
		[SerializeField] protected Idle _idle = default;
		[SerializeField] private List<HPEnemyAttackActionState> _attackActions = new List<HPEnemyAttackActionState>();
		[SerializeField] private float _minDelayBetweenAttacks = 3.0f;
		[SerializeField] private float _maxDelayBetweenAttacks = 3.0f;
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
			foreach (KeyValuePair<HPEnemyAttackActionState,float> mappingEntry in GetAttackActionProbabilityMapping(enemy))
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
			_attacksRemaining = (int) _attackChainAmount.Evaluate(r) - 1;
			_isChainAttack = _attacksRemaining > 0;
		}
		
		public void FinishAttack()
		{
			_lastAttackTime = Time.time;
			_attackDelay = Random.Range(_minDelayBetweenAttacks, _maxDelayBetweenAttacks);
		}
		
		private Dictionary<HPEnemyAttackActionState, float> GetAttackActionProbabilityMapping(Enemy enemy)
		{
			Dictionary<HPEnemyAttackActionState, float> mapping = new Dictionary<HPEnemyAttackActionState, float>();
			IEnumerable<HPEnemyAttackActionState> useableAttackActions = _attackActions.Where(c => c.CanBeUsed(enemy, _isChainAttack));
			
			float totalChance = useableAttackActions.Sum(c => c.Chance);
			foreach (HPEnemyAttackActionState enemyAttackActionState in useableAttackActions)
			{
				mapping.Add(enemyAttackActionState, enemyAttackActionState.Chance / totalChance);
			}

			return mapping;
		}
	}
}