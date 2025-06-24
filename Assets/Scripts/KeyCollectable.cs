using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollectedEvent : GameEvent<string> { }

public class KeyCollectable : MonoBehaviour, ICollectable
{
    [SerializeField] private string keyId = "MainKey";

    public void Collect()
    {
        var keyEvent = FindObjectOfType<KeyCollectedEvent>();
        keyEvent?.Raise(keyId);
        Destroy(gameObject);
    }
}

