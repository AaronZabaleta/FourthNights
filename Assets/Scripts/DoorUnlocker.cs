using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlocker : MonoBehaviour, IInteractable, IUsable
{
    [SerializeField] private bool requiresKey = true;
    [SerializeField] private string keyId = "MainKey";
    [SerializeField] private int requiredSwitches = 0;

    private bool hasKey = false;
    private int currentActivatedSwitches = 0;

    private void OnEnable()
    {
        var keyEvent = FindObjectOfType<KeyCollectedEvent>();
        if (keyEvent != null)
            keyEvent.OnEventRaised += OnKeyCollected;
    }

    private void OnDisable()
    {
        var keyEvent = FindObjectOfType<KeyCollectedEvent>();
        if (keyEvent != null)
            keyEvent.OnEventRaised -= OnKeyCollected;
    }

    private void OnKeyCollected(string collectedKeyId)
    {
        if (collectedKeyId == keyId)
            hasKey = true;
    }

    public void RegisterSwitchActivated()
    {
        currentActivatedSwitches++;
        TryUnlock();
    }

    public void Use()
    {
        RegisterSwitchActivated();
    }

    public void OnInteract()
    {
        TryUnlock();
    }

    private void TryUnlock()
    {
        if (requiresKey)
        {
            if (hasKey)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("La puerta está cerrada. Necesitas una llave.");
            }
        }
        else if (requiredSwitches > 0)
        {
            if (currentActivatedSwitches >= requiredSwitches)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log($"La puerta necesita activar {requiredSwitches - currentActivatedSwitches} palanca(s) más.");
            }
        }
        else
        {
            Debug.LogWarning("La puerta no tiene condiciones válidas para desbloquearse. Revisa la configuración.");
        }
    }
}


