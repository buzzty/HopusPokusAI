using UnityEngine;
using System.Collections;

namespace AISystem.HopusCopus
{
    public abstract class HopusCopusState : ScriptableEnemyState<EnemyActionFSM<HopusCopusState>, HopusCopusState>
    {
        public override HopusCopusState OnStateUpdate(EnemyActionFSM<HopusCopusState> fsm, Enemy enemy)
        {
            return OnStateUpdate(fsm as HopusCopusFsm, enemy);
        }

        public override void OnStateEnter(EnemyActionFSM<HopusCopusState> fsm, Enemy enemy)
        {
            OnStateEnter(fsm as HopusCopusFsm, enemy);
        }

        public override void OnStateExit(EnemyActionFSM<HopusCopusState> fsm, Enemy enemy)
        {
            OnStateExit(fsm as HopusCopusFsm, enemy);
        }

        protected virtual HopusCopusState OnStateUpdate(HopusCopusFsm fsm, Enemy enemy)
        {
            return this;
        }

        protected virtual void OnStateEnter(HopusCopusFsm fsm, Enemy enemy)
        {
        }

        protected virtual void OnStateExit(HopusCopusFsm fsm, Enemy enemy)
        {
        }
    }
}
