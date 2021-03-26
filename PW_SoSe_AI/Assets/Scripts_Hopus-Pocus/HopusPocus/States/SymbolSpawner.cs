using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AISystem.HopusPocus.States
{
  
    public class SymbolSpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform[] spawnPoints;

        [SerializeField] 
        private GameObject[] symbolAttack;

        [SerializeField] private float spawnTime;
        private float timer;

        private void Start()
        {
            timer = spawnTime;

            SpawnAttack();
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = spawnTime;
                SpawnAttack();
            }
        }

        private void SpawnAttack()
        {
           int randomSymbolIndex = Random.Range(0, symbolAttack.Length);
          GameObject newSymbolAttack = Instantiate(symbolAttack[randomSymbolIndex], spawnPoints[randomSymbolIndex]);
          //int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            //           newSymbolAttack.transform.position = ;
        }
        
        
    }
}
