using UnityEngine;
using System.Collections;

namespace AISystem.HopusPocus
{
    public abstract class HopusPocusState : ScriptableEnemyState<EnemyActionFSM<HopusPocusState>, HopusPocusState>
    {
        public override HopusPocusState OnStateUpdate(EnemyActionFSM<HopusPocusState> fsm, Enemy enemy)
        {
            return OnStateUpdate(fsm as HopusPocusFsm, enemy);
        }

        public override void OnStateEnter(EnemyActionFSM<HopusPocusState> fsm, Enemy enemy)
        {
            OnStateEnter(fsm as HopusPocusFsm, enemy);
        }

        public override void OnStateExit(EnemyActionFSM<HopusPocusState> fsm, Enemy enemy)
        {
            OnStateExit(fsm as HopusPocusFsm, enemy);
        }

        protected virtual HopusPocusState OnStateUpdate(HopusPocusFsm fsm, Enemy enemy)
        {
            return this;
        }

        protected virtual void OnStateEnter(HopusPocusFsm fsm, Enemy enemy)
        {
        }

        protected virtual void OnStateExit(HopusPocusFsm fsm, Enemy enemy)
        {
        }
    }
}
