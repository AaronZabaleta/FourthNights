using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActivator : MonoBehaviour, IUsable, IInteractable
{
    [SerializeField] private DoorUnlocker doorToActivate;

    public void Use()
    {
        doorToActivate?.RegisterSwitchActivated();
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
