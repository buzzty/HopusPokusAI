using UnityEngine;

namespace AISystem.HopusCopus
{
    public abstract class PhaseState : ScriptableObject, IAgentPhase
    {
        [SerializeField] private StateIdentifier _id;

        public StateIdentifier Id => _id;

        public virtual void InitPhase(EnemyPhaseFSM enemyPhaseFsm)
        {
        }

        public virtual void OnStateEnter(EnemyPhaseFSM phaseFSM, Enemy enemy)
        {
            enemy.Animator.SetInteger("Phase", phaseFSM.CurrentPhaseIndex);
        }

        public virtual void OnStateExit(EnemyPhaseFSM phaseFSM, Enemy enemy)
        {
        }

        public virtual bool OnStateUpdate(EnemyPhaseFSM phaseFSM, Enemy enemy)
        {
            // stay
            return false;
        }
    }
}
