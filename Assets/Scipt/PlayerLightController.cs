using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightController : MonoBehaviour
{
    public Light playerLight;
    public float lightDrainRate = 0.1f; 
    public bool IsLightOff => playerLight.intensity <= 0.01f;

    void Update()
    {
        if (playerLight.intensity > 0)
        {
            playerLight.intensity -= lightDrainRate * Time.deltaTime;
            playerLight.intensity = Mathf.Max(playerLight.intensity, 0);
        }
    }
}

