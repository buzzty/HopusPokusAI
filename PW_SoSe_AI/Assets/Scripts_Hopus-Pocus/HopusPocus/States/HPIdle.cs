using UnityEngine;

namespace AISystem.HopusPocus.States
{
    [CreateAssetMenu(menuName = "Cuphead/Bosses/HopusPocus/Attacks/IdleActionPhase01", fileName = "IdleActionPhase01", order = 0)]
    public class HPIdle : HopusPocusState
    {
        [SerializeField] private AudioClip _TwirlLoop;

        protected override void OnStateEnter(HopusPocusFSM fsm, Enemy enemy)
        {
            base.OnStateEnter(fsm, enemy);

            enemy.Animator.SetTrigger("Idle");
            enemy.PlayAudio(_TwirlLoop, true);
            Debug.Log("lol");
        }

        protected override HopusPocusState OnStateUpdate(HopusPocusFSM fsm, Enemy enemy)
        {
            if (!fsm.IsOnGlobalCooldown)
            {
                return fsm.GetNextState(enemy);
            }

            return fsm.Idle;
        }

        protected override void OnStateExit(HopusPocusFSM fsm, Enemy enemy)
        {
            base.OnStateExit(fsm, enemy);

            enemy.StopAudio();
        }
    }
}