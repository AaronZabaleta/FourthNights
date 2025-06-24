using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindController : MonoBehaviour
{
    [SerializeField] private MadnessScreenEffect madnessEffect;
    [SerializeField] private EnemyVisibilityChecker enemyChecker;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private PlayerLightController lightController;
    [SerializeField] private Player player;

    [SerializeField] private float mindValue = 100f;
    [SerializeField] private float mindDrainFromEnemy = 15f;
    [SerializeField] private float mindDrainFromDarkness = 10f;

    private void Update()
    {
        if (enemyChecker == null || playerCamera == null || madnessEffect == null || lightController == null || player == null)
            return;

        bool lightOff = lightController.IsLightOff;
        bool lookingAtEnemy = false;

        // Chequear visibilidad real del enemigo
        Vector3 dirToEnemy = (enemyChecker.transform.position - playerCamera.position).normalized;
        float dot = Vector3.Dot(playerCamera.forward, dirToEnemy);

        if (dot > 0.7f && enemyChecker.IsPartiallyVisible())
        {
            lookingAtEnemy = true;
        }

        float totalMindDrain = 0f;

        if (lookingAtEnemy)
            totalMindDrain += mindDrainFromEnemy;

        if (lightOff)
            totalMindDrain += mindDrainFromDarkness;

        if (lightOff && lookingAtEnemy)
            totalMindDrain += 10f;

        // Aplicar pérdida o recuperación de cordura
        if (totalMindDrain > 0)
        {
            mindValue = Mathf.Clamp(mindValue - totalMindDrain * Time.deltaTime, 0f, 100f);
        }
        else
        {
            mindValue = Mathf.Clamp(mindValue + 5f * Time.deltaTime, 0f, 100f); // subida lenta
        }

        madnessEffect.SetMindValue(mindValue);

        if (mindValue <= 0)
        {
            player.TakeDamage(0.1f * Time.deltaTime);
        }
    }
}