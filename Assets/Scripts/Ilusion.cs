using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ilusion : MonoBehaviour
{
    public AudioClip ghostSound;
    public GameObject smokePrefab; 
    private AudioSource audioSource;
    private GameObject spawnedSmoke;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (ghostSound != null)
            {
                audioSource.PlayOneShot(ghostSound);
            }

            if (smokePrefab != null)
            {
                spawnedSmoke = Instantiate(smokePrefab, transform.position, Quaternion.identity);
                
                spawnedSmoke.transform.SetParent(transform);
            }

            Destroy(gameObject, ghostSound.length);
        }
    }

    private void OnDestroy()
    {
        
        if (spawnedSmoke != null)
        {
            Destroy(spawnedSmoke);
        }
    }
}

