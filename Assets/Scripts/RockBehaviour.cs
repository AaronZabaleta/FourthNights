using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour, IInteractable
{
    private Animation rockAnimation;

    private void Awake()
    {
        rockAnimation = GetComponentInParent<Animation>();
    }

    public void OnInteract()
    {
        if (rockAnimation == null)
        {
            Debug.LogWarning("No se puede interactuar: no se encontró componente Animation");
            return;
        }

        if (!rockAnimation.GetClip("RockMove"))
        {
            Debug.LogWarning("El clip 'RockMove' no está asignado en el componente Animation");
            return;
        }

        if (rockAnimation.isPlaying)
        {
            Debug.Log("La animación ya se está reproduciendo");
            return;
        }

        Debug.Log("Reproduciendo animación 'RockMove'");
        rockAnimation.Play("RockMove");
    }
}
