using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFrame : MonoBehaviour
{
    private Player _parent;

    private void Start()
    {
        _parent = GetComponentInParent<Player>();
    }

    public void Attack()
    {

        _parent.Attack();

    }

    public void Interact()
    {

        _parent.Interact();

    }
}
