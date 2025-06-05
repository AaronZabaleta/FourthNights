using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public Transform SpawnFlecha;
    public GameObject Proyectil;
    public float Velocidad = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Disparo()
    {
        Instantiate(SpawnFlecha);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * Velocidad;

    }
}
