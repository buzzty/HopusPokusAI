using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Core.Utility;

public class AttackProjectileBehaviour : CachedMonoBehaviour
{
    protected virtual bool OwnerIsPlayer => false;
    [SerializeField] private int damage = 1;
    [SerializeField] private bool IsDestroyedOnImpact = true;

    private const float maxLifeTime = 10f;
    private float lifeTime = 0;

    protected virtual void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > maxLifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponentInParent<IDamageable>();
        if ((damageable != null) && (damageable.IsPlayer != OwnerIsPlayer))
        {
            bool dealtDamage = damageable.OnHit(damage);
           
            if (IsDestroyedOnImpact && dealtDamage)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
