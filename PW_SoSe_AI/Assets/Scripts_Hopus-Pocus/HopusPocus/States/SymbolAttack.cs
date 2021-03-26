using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AISystem.HopusPocus.States
{
    
    public class SymbolAttack : MonoBehaviour
    {
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;

        private float speed;

        private void Awake()
        {
            speed = Random.Range(minSpeed, maxSpeed);
        }

        private void Update()
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }
}
