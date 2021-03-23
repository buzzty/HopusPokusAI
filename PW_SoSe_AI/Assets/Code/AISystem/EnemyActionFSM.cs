using System;
using System.Collections.Generic;
using Core.Utility;
using UnityEngine;

namespace AISystem
{
	public abstract class EnemyActionFSM<TState> : CachedMonoBehaviour where TState : ScriptableEnemyState<EnemyActionFSM<TState>, TState>
	{
		[SerializeField] private FSMIdentifier _id;
		[SerializeField] private EnemyAnimationEventReceiver _animEventReceiver = default;

		public FSMIdentifier Id => _id;
		
		protected List<TState> _allStates = new List<TState>();
		protected TState _currentState;
		private bool _forceExit;
		protected override void Awake()
		{
			base.Awake();

			CollectAllStates();
			foreach (TState scriptableEnemyState in _allStates)
			{
				scriptableEnemyState.InitState();
			}
		}

		private void Start()
		{
			_animEventReceiver.OnAnimationDone += AnimationDone;
		}

		private void OnDestroy()
		{
			_animEventReceiver.OnAnimationDone -= AnimationDone;
		}

		private void AnimationDone(StateIdentifier stateIdentifier)
		{
			if (_currentState.Id.Equals(stateIdentifier) && _currentState is IAnimatorDriveable animatorDrivenState)
			{
				animatorDrivenState.AnimationDone();
			}
		}

		protected abstract void CollectAllStates();

		public void Tick(Enemy enemy)
		{
			TState baseAgentState = _currentState.OnStateUpdate(this, enemy);
			if ((_currentState != baseAgentState) || _forceExit)
			{
				_currentState.OnStateExit(this, enemy);
				_currentState = baseAgentState;
				_currentState.OnStateEnter(this, enemy);
				_forceExit = false;
			}
		}

		public void ForceExit()
		{
			_forceExit = true;
		}
	}
}