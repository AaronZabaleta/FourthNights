using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class KeyCollectable : MonoBehaviour, ICollectable, IInteractable
{
    [SerializeField] private string keyId = "MainKey";



    public void Collect()
    {
        var keyEvent = FindObjectOfType<KeyCollectedEvent>();
        Debug.Log($"Recolectando llave con ID: {keyId}");
        keyEvent?.Raise(keyId);
        Debug.Log("Evento levantado.");
        Destroy(gameObject);
    }

    public void OnInteract()
    {
        Collect();
    }
}

