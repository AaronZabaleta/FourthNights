using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{



    [Header("Rotación de cámara")]
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private float mouseSensitivity = 100f;

    [Header("Audio")]
    public AudioSource backgroundAudio;
    public AudioSource threatAudio;
    public float threatStartTime = 0.50f;
    public EnemyVisibilityChecker[] enemies;
    public float viewThreshold = 0.7f;

    [Header("Detección")]
    public float detectionRange = 10f;

    private float xRotation = 0f;
    private EnemyVisibilityChecker lastSeenEnemy;
    private bool isThreatPlaying = false;
  

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (backgroundAudio != null && !backgroundAudio.isPlaying)
            backgroundAudio.Play();
    }

    private void Update()
    {
        HandleCameraMovement();
        HandleEnemyAudio();
    }

    private void HandleCameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 50f);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void HandleEnemyAudio()
    {
        EnemyVisibilityChecker enemySeen = GetVisibleEnemy();

        if (enemySeen != null && enemySeen != lastSeenEnemy)
        {
            TriggerThreatAudio();
            lastSeenEnemy = enemySeen;
        }
    }

    private EnemyVisibilityChecker GetVisibleEnemy()
    {
        foreach (var enemy in enemies)
        {
            if (enemy == null || !enemy.gameObject.activeInHierarchy)
                continue;

            float distance = Vector3.Distance(enemy.transform.position, cameraHolder.position);
            if (distance > detectionRange)
                continue; // demasiado lejos
           

            Vector3 dirToEnemy = (enemy.transform.position - cameraHolder.position).normalized;
            float dot = Vector3.Dot(cameraHolder.forward, dirToEnemy);
            bool isVisible = dot > viewThreshold && enemy.IsPartiallyVisible();

            if (isVisible)
                return enemy;
        }

        return null;
    }

    private void TriggerThreatAudio()
    {
        if (isThreatPlaying) return;

        if (backgroundAudio != null && backgroundAudio.isPlaying)
            backgroundAudio.Pause();

        if (threatAudio != null)
        {
            if (threatAudio.clip != null && threatStartTime < threatAudio.clip.length)
            {
                threatAudio.Stop(); // <- 🔑 fuerza a resetear el audio
                threatAudio.time = threatStartTime;
                threatAudio.Play();
                Debug.Log($"🔊 Reproduciendo desde {threatStartTime}s. Confirmado: {threatAudio.time}s");
            }
            else
            {
                Debug.LogWarning("⛔ Tiempo inválido o clip no asignado.");
            }

            isThreatPlaying = true;
            StartCoroutine(WaitForThreatToFinish());
        }
    }

    private IEnumerator WaitForThreatToFinish()
    {
        while (threatAudio != null && threatAudio.isPlaying)
        {
            yield return null;
        }

        ReturnToBackgroundAudio(); 
    }

    private void ReturnToBackgroundAudio()
    {
        if (threatAudio != null && threatAudio.isPlaying)
            threatAudio.Stop();

        if (backgroundAudio != null && !backgroundAudio.isPlaying)
            backgroundAudio.Play();

        isThreatPlaying = false;
        lastSeenEnemy = null;

        Debug.Log("🎵 Enemigo fuera de vista: fondo restaurado.");
    }



}
