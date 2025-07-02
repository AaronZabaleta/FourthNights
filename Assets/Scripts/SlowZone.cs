using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    public float slowFactor = 0.5f;
    public AudioClip stickyFootstepClip;
  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            player.ModifySpeed(slowFactor);

            // Asigna el sonido de pisadas pegajoso
            GameAudioManager.Instance?.SetFootstepOverride(stickyFootstepClip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            player.ResetSpeed();

            // Restablece el sonido de pisadas normal
            GameAudioManager.Instance?.ResetFootstep();
        }
    }
}
