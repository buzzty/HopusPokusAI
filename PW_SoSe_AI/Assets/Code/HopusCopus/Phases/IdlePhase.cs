﻿using UnityEngine;
using UnityEditor;

namespace AISystem.HopusCopus.Phases
{
    [CreateAssetMenu(menuName = "Cuphead/Bosses/HopusCopus/States/SwitchPhase", fileName = "SwitchPhase", order = 0)]
    public class SwitchPhase : AnimatorDrivenPhase
    {
        public override void OnStateEnter(EnemyPhaseFSM phaseFSM, Enemy enemy)
        {
            base.OnStateEnter(phaseFSM, enemy);
            enemy.Animator.SetTrigger("SwitchPhase");
        }

        public override void OnStateExit(EnemyPhaseFSM phaseFSM, Enemy enemy)
        {
            base.OnStateExit(phaseFSM, enemy);
            enemy.Animator.SetTrigger("SwitchPhase");
        }
    }
}