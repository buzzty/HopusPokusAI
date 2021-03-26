using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem.HopusPocus.States
{
    [CreateAssetMenu(menuName = "Cuphead/Bosses/HopusPocus/Attacks/SymbolSpawner", fileName = "SymbolSpawner",
        order = 0)]
    public class SymbolSpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform[] spawnPoints;

        [SerializeField] 
        private GameObject[] symbolAttack;
    }
}
