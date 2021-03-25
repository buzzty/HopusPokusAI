using UnityEngine;

namespace AISystem.HopusPocus.Phases
{
    [CreateAssetMenu(menuName = "Cuphead/Bosses/HopusPocus/States/SpawnPhase", fileName = "SpawnPhase", order = 0)]
    public class HPSpawnPhase : HPAnimatorDrivenPhase
    {
        public override void OnStateEnter(HPEnemyPhaseFSM phaseFsm, Enemy enemy)
        {
            base.OnStateEnter(phaseFsm, enemy);
            enemy.Animator.SetTrigger("SetSpawn");
        }
    }
}
