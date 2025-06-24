using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimation : MonoBehaviour
{
    private IAttackable attacker;

    private void Awake()
    {
        attacker = GetComponentInParent<IAttackable>();
    }

    public void TriggerAttack()
    {
        attacker?.Attack();
    }
}