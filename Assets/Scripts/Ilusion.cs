using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ilusion : MonoBehaviour
{
    public AudioClip ghostSound;
    private AudioSource audioSource;

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
            Destroy(gameObject, ghostSound.length);
        }
    }
}

