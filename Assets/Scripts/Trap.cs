using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private int damage = 9999;
    [SerializeField] private LayerMask attackMask;

    [Header("VFX")]
    public GameObject explosionVFXPrefab; 
    public GameEvent<Vector3> onExplosion;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;
        if (((1 << other.gameObject.layer) & attackMask) == 0) return;

        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
        }

       
        onExplosion?.Raise(transform.position);

        
        if (GameAudioManager.Instance != null && GameAudioManager.Instance.explosionClip != null)
        {
            GameAudioManager.Instance.PlaySFX(GameAudioManager.Instance.explosionClip);
        }

        
        if (explosionVFXPrefab != null)
        {
            GameObject vfx = Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity);
            Destroy(vfx, 3f); 
        }

        hasTriggered = true;
        Destroy(gameObject, 1.5f);
    }
}
