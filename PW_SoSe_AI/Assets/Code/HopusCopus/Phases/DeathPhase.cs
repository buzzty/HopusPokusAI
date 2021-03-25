using UnityEngine;

namespace AISystem.HopusCopus.Phases
{
    [CreateAssetMenu(menuName = "Cuphead/Bosses/HopusCopus/States/DeathPhase", fileName = "DeathPhase", order = 0)]
    public class DeathPhase : PhaseState
    {
        public override void OnStateEnter(EnemyPhaseFSM phaseFsm, Enemy enemy)
        {
            base.OnStateEnter(phaseFsm, enemy);
            enemy.Animator.SetTrigger("SetDead");
        }

        public override bool OnStateUpdate(EnemyPhaseFSM phaseFsm, Enemy enemy)
        {
            // Death is last state, stay here
            return false;
        }
    }
}