using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindController : MonoBehaviour
{
    [SerializeField] private MadnessScreenEffect madnessEffect;
    [SerializeField] private EnemyVisibilityChecker[] enemyCheckers;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private PlayerLightController lightController;
    [SerializeField] private Player player;
    [SerializeField] private int damagePerTick = 1;
    [SerializeField] private float damageInterval = 1f;


    private float damageTimer = 0f;

    [SerializeField] private float mindValue = 100f;
    [SerializeField] private float mindDrainFromEnemy = 15f;
    [SerializeField] private float mindDrainFromDarkness = 10f;
    [SerializeField] private float viewThreshold = 0.7f;
    [SerializeField] private float detectionRange = 10f;

    private void Update()
    {
        if (madnessEffect == null || playerCamera == null || lightController == null || player == null || enemyCheckers == null)
            return;

        bool lightOff = lightController.IsLightOff;
        bool lookingAtEnemy = false;

        foreach (var checker in enemyCheckers)
        {
            if (checker == null || !checker.gameObject.activeInHierarchy) continue;

            float distance = Vector3.Distance(checker.transform.position, playerCamera.position);
            if (distance > detectionRange) continue;

            Vector3 dirToEnemy = (checker.transform.position - playerCamera.position).normalized;
            float dot = Vector3.Dot(playerCamera.forward, dirToEnemy);
            bool isVisible = dot > viewThreshold && checker.IsPartiallyVisible();

            if (isVisible)
            {
                lookingAtEnemy = true;
                break; 
            }
        }

        float totalMindDrain = 0f;

        if (lookingAtEnemy)
            totalMindDrain += mindDrainFromEnemy;

        if (lightOff)
            totalMindDrain += mindDrainFromDarkness;

        if (lightOff && lookingAtEnemy)
            totalMindDrain += 10f;

       
        if (totalMindDrain > 0)
        {
            mindValue = Mathf.Clamp(mindValue - totalMindDrain * Time.deltaTime, 0f, 100f);
            madnessEffect.SetGreenEffect(true);
        }
        else
        {
            mindValue = Mathf.Clamp(mindValue + 5f * Time.deltaTime, 0f, 100f);
            madnessEffect.SetGreenEffect(false);
        }

        madnessEffect.SetMindValue(mindValue);

        if (mindValue <= 0)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                player.TakeDamage(damagePerTick);
                damageTimer = 0f;
            }
        }
        else
        {
            damageTimer = 0f; 
        }
    }
}