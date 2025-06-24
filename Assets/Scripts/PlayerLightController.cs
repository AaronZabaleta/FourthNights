using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightController : MonoBehaviour
{
    [Header("Light Settings")]
    public Light playerLight;
    public float lightDrainRate = 0.1f;

    public GameEvent<float> onLightLevelChanged; // Opcional: notifica cambio de luz

    public bool IsLightOff => playerLight.intensity <= 0.01f;

    private void Update()
    {
        if (playerLight == null) return;

        if (playerLight.intensity > 0)
        {
            playerLight.intensity -= lightDrainRate * Time.deltaTime;
            playerLight.intensity = Mathf.Max(playerLight.intensity, 0);

            onLightLevelChanged?.Raise(playerLight.intensity);
        }
    }
}

