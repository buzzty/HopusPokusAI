﻿using UnityEngine;

namespace AISystem.HopusPocus.Phases
{
    [CreateAssetMenu(menuName = "Cuphead/Bosses/HopusPocus/States/DeathPhase", fileName = "DeathPhase", order = 0)]
    public class HPDeathPhase : HPPhaseState
    {
        public override void OnStateEnter(HPEnemyPhaseFSM phaseFsm, Enemy enemy)
        {
            base.OnStateEnter(phaseFsm, enemy);
            enemy.Animator.SetTrigger("SetDead");
        }

        public override bool OnStateUpdate(HPEnemyPhaseFSM phaseFsm, Enemy enemy)
        {
            // Death is last state, stay here
            return false;
        }
    }
}