using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour, Interactable
{
   
    private Animation _animation;

    private void Awake()
    {
        _animation = GetComponentInParent<Animation>();

        
    }

    public void OnInteract()
    {
        if (_animation == null)
        {
            Debug.LogWarning("No se puede interactuar");
            return;
        }

        if (!_animation.GetClip("RockMove"))
        {
            Debug.LogWarning("El clip 'RockMove' no está");
            return;
        }

        if (_animation.isPlaying)
        {
            Debug.Log("La animación ya está");
            return;
        }

        Debug.Log("Ejecutando _animation.Play(\"RockMove\")");
        _animation.Play("RockMove");
    }
}
