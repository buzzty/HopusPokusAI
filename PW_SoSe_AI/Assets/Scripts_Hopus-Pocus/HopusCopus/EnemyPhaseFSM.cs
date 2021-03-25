using System;
using System.Collections.Generic;
using AISystem.CagneyCarnation.Phases;
using Core.Utility;
using UnityEngine;

namespace AISystem.HopusPocus
{
    public class EnemyPhaseFSM : CachedMonoBehaviour
    {
        public static event Action<int> OnPhaseSwitched;

        [SerializeField] private SpawnPhase _spawnPhase;
        [SerializeField] private List<ActivePhase> _attackingPhases = new List<ActivePhase>();
        [SerializeField] private SwitchPhase _switchPhase;
        [SerializeField] private DeathPhase _deathPhase;
        [SerializeField] private EnemyAnimationEventReceiver _animEventReceiver = default;
        public int CurrentPhaseIndex => _currentPhaseIndex;
        private List<PhaseState> _phases = new List<PhaseState>();
        private int _currentPhaseIndex;
        private IAgentPhase CurrentPhase => _phases[_currentPhaseIndex];

        protected override void Awake()
        {
            base.Awake();

            _phases.Add(_spawnPhase);

            for (int i = 0; i < _attackingPhases.Count; i++)
            {
                _phases.Add(_attackingPhases[i]);
                _attackingPhases[i].InitPhase(this);
                // last phase doesnt need switch phase, as it directly transitions -> death
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
            if (CurrentPhase.Id.Equals(stateIdentifier) && CurrentPhase is AnimatorDrivenPhase animatorDrivenPhase)
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
