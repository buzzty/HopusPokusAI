using System.Linq;
using UnityEngine;

namespace AISystem.CagneyCarnation.Phases
{
	[CreateAssetMenu(menuName = "Cuphead/Bosses/CagneyCarnation/States/ActivePhase", fileName = "ActivePhase", order = 0)]
	public class ActivePhase : PhaseState
	{
		// [SerializeField] private List<AgentState> _agentStates = new List<AgentState>();
		[SerializeField] private float _normalizedHPThreshold = 0.5f;
		[SerializeField] private FSMIdentifier _phaseFSM = default;
		
		private EnemyActionFSM<CagneyCarnationState> _fsm;
		
		public override void InitPhase(EnemyPhaseFSM enemyPhaseFsm)
		{
			base.InitPhase(enemyPhaseFsm);

			_fsm = enemyPhaseFsm.GetComponentsInChildren<EnemyActionFSM<CagneyCarnationState>>().FirstOrDefault(fsm => fsm.Id.Equals(_phaseFSM));
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