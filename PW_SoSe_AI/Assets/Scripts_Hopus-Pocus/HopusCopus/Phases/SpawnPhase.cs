using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem.HopusPocus.Phases
{
    [CreateAssetMenu(menuName = "Cuphead/Bosses/HopusCopus/States/SpawnPhase", fileName = "SpawnPhase", order = 0)]
    public class SpawnPhase : AnimatorDrivenPhase
    {
        public override void OnStateEnter(EnemyPhaseFSM phaseFsm, Enemy enemy)
        {
            base.OnStateEnter(phaseFsm, enemy);
            enemy.Animator.SetTrigger("SetSpawn");
        }
    }
}
