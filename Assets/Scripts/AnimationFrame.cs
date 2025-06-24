using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFrame : MonoBehaviour
{
    private IAttackable attacker;
    private IInteractable interactor;

    private void Start()
    {
        attacker = GetComponentInParent<IAttackable>();
        interactor = GetComponentInParent<IInteractable>();
    }

    public void Attack()
    {
        attacker?.Attack();
    }

    public void OnInteract()
    {
        interactor?.OnInteract();
    }
}

