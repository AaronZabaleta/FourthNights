using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaPisable : MonoBehaviour
{
    public Flecha FlechaScript;

    private GameObject TrampaPresion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            FlechaScript.Disparo();
            Debug.Log("Fiuuuuum");
        }
    }

   
}
