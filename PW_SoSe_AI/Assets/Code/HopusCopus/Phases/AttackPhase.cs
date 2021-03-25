using UnityEngine;
using UnityEditor;

namespace AISystem.HopusCopus.Phases
{
    [CreateAssetMenu(menuName = "Cuphead/Bosses/HopusCopus/States/ActivePhase", fileName = "ActivePhase", order = 0)]
    public class AttackPhase : PhaseState
    {
        // [SerializeField] private List<AgentState> _agentStates = new List<AgentState>();
        [SerializeField] private float _normalizedHPThreshold = 0.5f;
        [SerializeField] private FSMIdentifier _phaseFSM = default;

        private EnemyActionFSM<HopusCopus> _fsm;

        public override void InitPhase(EnemyPhaseFSM enemyPhaseFsm)
        {
            base.InitPhase(enemyPhaseFsm);

            _fsm = enemyPhaseFsm.GetComponentsInChildren<EnemyActionFSM<HopusCopusState>>().FirstOrDefault(fsm => fsm.Id.Equals(_phaseFSM));
        }

        public override bool OnStateUpdate(EnemyPhaseFSM phaseFSM, Enemy enemy)
        {
            if (_fsm != null)
            {
                _fsm.Tick(enemy);
            }

            if (enemy.NormalizedHealth <= _normalizedHPThreshold)
            {
                // next
                return true;
            }

            return base.OnStateUpdate(phaseFSM, enemy);
        }
    }
}