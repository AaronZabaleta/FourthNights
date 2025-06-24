using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaPisable : MonoBehaviour
{
    public Flecha flechaScript;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            flechaScript.Disparo();
            Debug.Log("Flecha disparada desde trampa");
        }
    }
}
