using UnityEngine;

namespace AISystem.HopusPocus
{
    public abstract class HPPhaseState : ScriptableObject, HPIAgentPhase
    {
        [SerializeField] private StateIdentifier _id;

        public StateIdentifier Id => _id;

        public virtual void InitPhase(HPEnemyPhaseFSM enemyPhaseFsm)
        {
        }

        public virtual void OnStateEnter(HPEnemyPhaseFSM phaseFSM, Enemy enemy)
        {
            enemy.Animator.SetInteger("Phase", phaseFSM.CurrentPhaseIndex);
        }

        public virtual void OnStateExit(HPEnemyPhaseFSM phaseFSM, Enemy enemy)
        {
        }

        public virtual bool OnStateUpdate(HPEnemyPhaseFSM phaseFSM, Enemy enemy)
        {
            // stay
            return false;
        }
    }
}
