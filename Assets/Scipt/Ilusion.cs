using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ilusion : MonoBehaviour
{
   
        public GameObject Player;
        public AudioClip ghostSound; 

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (ghostSound != null)
                {
                    _audioSource.PlayOneShot(ghostSound);
                }

                Destroy(gameObject, ghostSound.length); 
            }
        }
    
}
