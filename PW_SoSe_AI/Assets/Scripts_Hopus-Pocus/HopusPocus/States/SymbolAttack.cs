using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AISystem.HopusPocus.States
{
    [CreateAssetMenu(menuName = "Cuphead/Bosses/HopusPocus/Attacks/SymbolAttack", fileName = "SymbolAttack", order = 0)]
    public class SymbolAttack : HPEnemyAttackActionState
    {
        [SerializeField]
        private float speed;
        

        private void Update()
        {
            
          //  GameObject.transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }
}
