using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActivator : MonoBehaviour, IUsable, IInteractable
{
    [SerializeField] private DoorUnlocker doorToActivate;
    private bool hasBeenUsed = false; 

    public void Use()
    {
        if (hasBeenUsed) return; 

        doorToActivate?.RegisterSwitchActivated();
        hasBeenUsed = true; 
    }

    public void OnInteract()
    {
        Use();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Use();
            gameObject.SetActive(false); 
        }
    }
}
