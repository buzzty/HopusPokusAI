using UnityEngine;

namespace AISystem.HopusPocus.States
{
    [CreateAssetMenu(menuName = "Cuphead/Bosses/HopusPocus/Attacks/IdleActionPhase01", fileName = "IdleActionPhase01", order = 0)]
    public class HPIdle : HopusPocusState
    {
        [SerializeField] private AudioClip _TwirlLoop;

        protected override void OnStateEnter(HopusPocusFSM fsm, Enemy enemy)
        {
            Debug.Log("Enter Idle");
            base.OnStateEnter(fsm, enemy);

            Debug.Log("Playing Anim");
            enemy.Animator.SetTrigger("Idle");
            Debug.Log("Playing Audio: " + _TwirlLoop);
            enemy.PlayAudio(_TwirlLoop, true);
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