using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassLvl : MonoBehaviour
{
    public string nombreLevel; 

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreLevel);
            Debug.Log("Fiuuuuum");
        }
    }




}
