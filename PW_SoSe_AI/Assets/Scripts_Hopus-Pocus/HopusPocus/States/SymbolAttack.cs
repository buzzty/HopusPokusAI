using System;
using UnityEngine;


namespace AISystem.HopusPocus.States
{
    [CreateAssetMenu(menuName = "Cuphead/Bosses/HopusPocus/Attacks/SymbolAttack", fileName = "SymbolAttack", order = 0)]
    public class SymbolAttack : HPEnemyAttackActionState
    {
        [SerializeField]
        private CardProjectileBehaviour cardProjectilePrefab;

        protected override void OnStateEnter(HopusPocusFSM fsm, Enemy enemy)
        {
            Instantiate(cardProjectilePrefab);
        }
        

        protected override HopusPocusState OnStateUpdate(HopusPocusFSM fsm, Enemy enemy)
        {
            return fsm.Idle;
        }
    }
}
