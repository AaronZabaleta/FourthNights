using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject projectilePrefab;
    public float velocidad = 15f;

    public void Disparo()
    {
        GameObject flecha = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);

        if (flecha.TryGetComponent(out Rigidbody rb))
        {
            rb.velocity = spawnPoint.forward * velocidad;
        }
    }
}
