using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightController : MonoBehaviour
{
    [Header("Light Settings")]
    [SerializeField] private float rechargeRate = 10f;
    [SerializeField] private float maxBattery = 100f;
    [SerializeField] private float currentBattery = 50f;
    [SerializeField] private float maxIntensity = 6.33f;
    [SerializeField] private float minIntensity = 0f;    
    [SerializeField] private float intensityRechargeSpeed = 2f;
    public Light playerLight;
    public float lightDrainRate = 0.1f;
    public bool IsRecharging { get; private set; }

    public GameEvent<float> onLightLevelChanged; // notifica cambio de luz

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

        if (IsRecharging)
        {
            currentBattery += rechargeRate * Time.deltaTime;
            currentBattery = Mathf.Clamp(currentBattery, 0f, maxBattery);

            // Aumentar intensidad según batería
            playerLight.intensity += intensityRechargeSpeed * Time.deltaTime;
            playerLight.intensity = Mathf.Clamp(playerLight.intensity, minIntensity, maxIntensity);

            onLightLevelChanged?.Raise(playerLight.intensity);
        }
        else
        {
            // Drenaje pasivo si no se está recargando
            playerLight.intensity -= lightDrainRate * Time.deltaTime;
            playerLight.intensity = Mathf.Clamp(playerLight.intensity, minIntensity, maxIntensity);

            onLightLevelChanged?.Raise(playerLight.intensity);
        }
    }

    public void StartRecharge()
    {
        IsRecharging = true;
    }

    public void StopRecharge()
    {
        IsRecharging = false;
    }
}

