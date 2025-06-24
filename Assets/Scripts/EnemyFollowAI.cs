using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowAI : MonoBehaviour
{
    [Header("Configuración")]
    public float velocidad = 2f;
    public float distanciaMinima = 2f;

    [Header("Estado")]
    public bool seguimientoActivo = false;
    public bool estaAtacando = false;

    private Transform target;
    private Rigidbody rb;

    private void Awake()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            target = playerObj.transform;

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (target == null) return;

        float distancia = Vector3.Distance(transform.position, target.position);

        if (seguimientoActivo && distancia > distanciaMinima)
        {
            SeguirJugador();
        }
        else if (distancia <= distanciaMinima && !estaAtacando)
        {
            IniciarAtaque();
        }
    }

    private void SeguirJugador()
    {
        transform.LookAt(target);
        Vector3 direccion = (target.position - transform.position).normalized;
        rb.MovePosition(transform.position + direccion * velocidad * Time.deltaTime);
    }

    private void IniciarAtaque()
    {
        estaAtacando = true;

        if (TryGetComponent(out IAttackable atacante))
        {
            atacante.Attack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !estaAtacando)
        {
            seguimientoActivo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            seguimientoActivo = false;
        }
    }
}

