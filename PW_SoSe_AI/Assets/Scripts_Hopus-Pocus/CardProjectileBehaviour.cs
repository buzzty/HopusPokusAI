using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardProjectileBehaviour : AttackProjectileBehaviour
{
    [SerializeField]
    private float speed;

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Symbols"))
        {
            Destroy(gameObject);
        }
    }
    
    
}
