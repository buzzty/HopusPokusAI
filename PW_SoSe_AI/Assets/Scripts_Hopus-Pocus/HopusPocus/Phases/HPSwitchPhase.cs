using UnityEngine;
using UnityEditor;

namespace AISystem.HopusPocus.Phases
{
    [CreateAssetMenu(menuName = "Cuphead/Bosses/HopusPocus/States/SwitchPhase", fileName = "SwitchPhase", order = 0)]
    public class HPSwitchPhase : HPAnimatorDrivenPhase
    {
        public override void OnStateEnter(HPEnemyPhaseFSM phaseFSM, Enemy enemy)
        {
            base.OnStateEnter(phaseFSM, enemy);
            enemy.Animator.SetTrigger("SwitchPhase");
        }

        public override void OnStateExit(HPEnemyPhaseFSM phaseFSM, Enemy enemy)
        {
            base.OnStateExit(phaseFSM, enemy);
            enemy.Animator.SetTrigger("SwitchPhase");
        }
    }
}