using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsable
{
    void Use();
}

public interface IDamageable
{
    void TakeDamage(float dmg);
}

public interface IAttackable
{
    void Attack();
}

public interface IInteractable
{
    void OnInteract();
}

public interface ICollectable
{
    void Collect();
}


