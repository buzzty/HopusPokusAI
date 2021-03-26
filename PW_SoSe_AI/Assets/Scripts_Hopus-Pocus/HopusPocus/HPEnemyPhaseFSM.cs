using System;
using System.Collections.Generic;
using AISystem.HopusPocus.Phases;
using Core.Utility;
using UnityEngine;

namespace AISystem.HopusPocus
{
    public class HPEnemyPhaseFSM : CachedMonoBehaviour
    {
        public static event Action<int> OnPhaseSwitched;

        [SerializeField] private HPSpawnPhase _spawnPhase;
        [SerializeField] private List<HPAttackPhase> _attackingPhases = new List<HPAttackPhase>();
        [SerializeField] private HPSwitchPhase _switchPhase;
        [SerializeField] private HPDeathPhase _deathPhase;
        [SerializeField] private HPEnemyAnimationEventReceiver _animEventReceiver = default;
        public int CurrentPhaseIndex => _currentPhaseIndex;
        private List<HPPhaseState> _phases = new List<HPPhaseState>();
        private int _currentPhaseIndex;
        private HPIAgentPhase CurrentPhase => _phases[_currentPhaseIndex];

        protected override void Awake()
        {
            base.Awake();

            _phases.Add(_spawnPhase);

            for (int i = 0; i < _attackingPhases.Count; i++)
            {
                _phases.Add(_attackingPhases[i]);
                _attackingPhases[i].InitPhase(this);
                //last phase doesnt need switch phase, as it directly transitions -> death
                if (i + 1 < _attackingPhases.Count)
                {
                    _phases.Add(_switchPhase);
                }
            }

            _phases.Add(_deathPhase);
            _currentPhaseIndex = 0;
        }

        private void Start()
        {
            _animEventReceiver.OnAnimationDone += AnimationDone;
        }

        private void OnDestroy()
        {
            _animEventReceiver.OnAnimationDone -= AnimationDone;
        }

        public void Tick(Enemy enemy)
        {
            bool switchPhase = CurrentPhase.OnStateUpdate(this, enemy);
            if (switchPhase)
            {
                CurrentPhase.OnStateExit(this, enemy);
                _currentPhaseIndex++;
                CurrentPhase.OnStateEnter(this, enemy);

                OnPhaseSwitched?.Invoke(_currentPhaseIndex);
            }
        }

        private void AnimationDone(StateIdentifier stateIdentifier)
        {
            if (CurrentPhase.Id.Equals(stateIdentifier) && CurrentPhase is HPAnimatorDrivenPhase animatorDrivenPhase)
            {
                animatorDrivenPhase.AnimationDone();
            }
        }

        public void Init(Enemy enemy)
        {
            CurrentPhase.OnStateEnter(this, enemy);
        }
    }
}
