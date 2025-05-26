using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimation : MonoBehaviour
{
    private Enemy _parent;

    private void Start()
    {
        _parent = GetComponentInParent<Enemy>();
    }

    public void Attack()
    {

        _parent.Attack();

    }

   
}
