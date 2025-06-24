using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour, IAttackable
{
    [Header("Gameplay")]
    [SerializeField] private int damage = 9999;
    [SerializeField] private float attackDistance = 2.0f;
    [SerializeField] private LayerMask attackMask;
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private float radius;

    [Header("Audio")]
    [SerializeField] private AudioClip bigExplosion;
    private AudioSource audioSource;

    [Header("VFX")]
    public GameEvent<Vector3> onExplosion; // evento para notificar que explotó

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Attack()
    {
        if (Physics.Raycast(rayOrigin.position, transform.forward, out RaycastHit hit, attackDistance, attackMask))
        {
            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
        }

        onExplosion?.Raise(transform.position); // notifica a quien escuche
        if (bigExplosion != null)
            audioSource.PlayOneShot(bigExplosion);

        Destroy(gameObject, 1.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            Attack();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
