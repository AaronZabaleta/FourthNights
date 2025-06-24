using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, IAttackable
{
    [Header("Animator")]
    [SerializeField] private string atkTriggerName = "onAttack";
    [SerializeField] private string isWalkName = "IsWalk";

    [Header("Gameplay")]
    [SerializeField] private int attackDamage = 1;

    [Header("Movement & Detection")]
    [SerializeField] private float radius = 10f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private LayerMask attackMask;
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private Transform target;

    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!target || !healthComponent.IsAlive) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= radius)
        {
            Vector3 lookDir = target.position - transform.position;
            lookDir.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 300 * Time.deltaTime);

            if (distance > attackDistance)
            {
                animator.SetBool(isWalkName, true);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else
            {
                animator.SetBool(isWalkName, false);
                animator.SetTrigger(atkTriggerName);
            }
        }
        else
        {
            animator.SetBool(isWalkName, false);
        }
    }

    public void Attack()
    {
        Debug.DrawRay(rayOrigin.position, transform.forward * attackDistance, Color.red, 1f);
        if (Physics.Raycast(rayOrigin.position, transform.forward, out RaycastHit hit, attackDistance, attackMask))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                Debug.Log("Enemy is damaging: " + damageable);
                damageable.TakeDamage(attackDamage);
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }
}


