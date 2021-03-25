using AISystem.HopusPocus;
using Core.Utility;
using UnityEngine;

namespace AISystem
{
    public abstract class HPEnemyAttackActionState : HopusPocusState, IAnimatorDriveable
    {
        [SerializeField] private float _attackCooldown = 3f;
        [SerializeField] private bool _limitUsageByHP = true;
        [SerializeField] [MinMaxFloat(0f, 1f)] private MinMaxFloat _hpMinMax = new MinMaxFloat(0f, 1f);
        [SerializeField] private int _chance = 1;
        public int Chance => _chance;
        public bool IsOnCooldown => _lastTimeUsed + _attackCooldown > Time.time;

        protected bool _isAnimationDone;
        private float _lastTimeUsed = 0;

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

        protected override void OnStateExit(HopusPocusFsm fsm, Enemy enemy)
        {
            base.OnStateExit(fsm, enemy);

            _lastTimeUsed = Time.time;
            fsm.FinishAttack();
        }

        protected override HopusPocusState OnStateUpdate(HopusPocusFsm fsm, Enemy enemy)
        {
            if (_isAnimationDone)
            {
                fsm.ForceExit();
                return fsm.IsChainAttack ? fsm.GetNextState(enemy) : fsm.Idle;
            }

            return base.OnStateUpdate(fsm, enemy);
        }

        protected override void OnStateEnter(HopusPocusFsm fsm, Enemy enemy)
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
