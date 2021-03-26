using UnityEngine;

namespace AISystem.HopusPocus.States
{
    [CreateAssetMenu(menuName = "Cuphead/Bosses/HopusPocus/Attacks/IdleActionPhase01", fileName = "IdleActionPhase01", order = 0)]
    public class HPIdle : HopusPocusState
    {
        protected override void OnStateEnter(HopusPocusFSM fsm, Enemy enemy)
        {
            base.OnStateEnter(fsm, enemy);

            enemy.Animator.SetTrigger("Idle");
        }

        protected override HopusPocusState OnStateUpdate(HopusPocusFSM fsm, Enemy enemy)
        {
            if (!fsm.IsOnGlobalCooldown)
            {
                return fsm.GetNextState(enemy);
            }

            return fsm.Idle;
        }
    }
}