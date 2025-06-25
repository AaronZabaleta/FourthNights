using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlocker : MonoBehaviour, IInteractable, IUsable
{
    [SerializeField] private bool requiresKey = true;
    [SerializeField] private string keyId = "MainKey";
    [SerializeField] private int requiredSwitches = 0;
    [SerializeField] private GameObject enemySpawner;

    private bool hasSpawnedEnemies = false;
    private bool hasKey = false;
    private int currentActivatedSwitches = 0;
    private KeyCollectedEvent keyEvent;

    private void OnEnable()
    {
        keyEvent = FindObjectOfType<KeyCollectedEvent>();
        if (keyEvent != null)
        {
            Debug.Log("Suscribiendo puerta al evento de llave.");
            keyEvent.OnEventRaised += OnKeyCollected;
        }
    }

    private void Start()
    {
        if (keyEvent == null) 
        {
            keyEvent = FindObjectOfType<KeyCollectedEvent>();
            if (keyEvent != null)
                keyEvent.OnEventRaised += OnKeyCollected;
        }
    }

    private void OnDisable()
    {
        if (keyEvent != null)
            keyEvent.OnEventRaised -= OnKeyCollected;
    }

    private void OnKeyCollected(string collectedKeyId)
    {
        Debug.Log($"Llave recibida en puerta: {collectedKeyId}");

        if (!hasSpawnedEnemies && enemySpawner != null)
        {
            Debug.Log("Activando enemigo desde DoorUnlocker.");
            enemySpawner.SetActive(true);
            hasSpawnedEnemies = true;
        }

        if (collectedKeyId == keyId)
        {
            hasKey = true;
            
        }
    }

    public void RegisterSwitchActivated()
    {
        if (!hasSpawnedEnemies)
        {
            enemySpawner?.SetActive(true);  
            hasSpawnedEnemies = true;       
        }

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


