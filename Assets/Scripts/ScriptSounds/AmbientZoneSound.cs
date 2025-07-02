
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AmbientZoneSound : MonoBehaviour
{
    public enum AmbientType { Tar, Water }
    public AmbientType ambientType = AmbientType.Tar;

    public float delayBeforeLoop = 0.1f;

    private bool playerInside = false;
    private float timer = 0f;

    private void Update()
    {
        if (playerInside)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                PlayAmbientLoop();
                timer = delayBeforeLoop;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            PlayAmbientLoop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            GameAudioManager.Instance?.StopLoop();
        }
    }

    private void PlayAmbientLoop()
    {
        if (GameAudioManager.Instance == null) return;

        switch (ambientType)
        {
            case AmbientType.Tar:
                GameAudioManager.Instance.StartLoop(GameAudioManager.Instance.stickyClip);
                break;
            case AmbientType.Water:
                GameAudioManager.Instance.StartLoop(GameAudioManager.Instance.waterClip);
                break;
        }
    }
}
