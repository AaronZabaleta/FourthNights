using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    protected HealthComponent healthComponent;

    protected virtual void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    public virtual void TakeDamage(float dmg)
    {
        healthComponent.TakeDamage(Mathf.RoundToInt(dmg));
        if (!healthComponent.IsAlive)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}