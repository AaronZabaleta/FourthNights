using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seguimientoenemigo : MonoBehaviour
{
    public Transform Player;
    public float VelocidadEnemigo;
    public float DistanciaMinima;



    public Player PlayerScript;

    public Rigidbody Rigidbody;

    public bool ActivaSeguimiento = false;
    public bool Atacando = false;   

   
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (ActivaSeguimiento == true)
        {
            Seguimiento();

        }
        if (Vector3.Distance(transform.position, Player.transform.position) < DistanciaMinima)
        {
            ActivaSeguimiento = false;

            Ataque();
        }
    }

    public void Ataque()
    {
       Atacando = true;

        
    }

    public void Seguimiento()
    {
        transform.LookAt(Player);
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, VelocidadEnemigo * Time.deltaTime);


    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && Vector3.Distance(transform.position, Player.transform.position) > DistanciaMinima)
        {
            ActivaSeguimiento = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActivaSeguimiento = false;
        }
    }
}

