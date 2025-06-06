using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindController : MonoBehaviour
{
    public MindBar mindBar;
    public EnemyVisibilityChecker enemyChecker;
    public Transform playerCamera;
    public PlayerLightController lightController;
    public Player player;
    public LifeBar healthBar;

    public float mindDrainFromEnemy = 5f;
    public float mindDrainFromDarkness = 3f;

    void Update()
    {
        bool lightOff = lightController.IsLightOff;

        Vector3 dirToEnemy = (enemyChecker.transform.position - playerCamera.position).normalized;
        float dot = Vector3.Dot(playerCamera.forward, dirToEnemy);
        bool lookingAtEnemy = dot > 0.7f && enemyChecker.IsPartiallyVisible();

        float totalMindDrain = 0f;

        if (lookingAtEnemy)
        {
            totalMindDrain += mindDrainFromEnemy;
        }

        if (lightOff)
        {
            totalMindDrain += mindDrainFromDarkness;
        }

        if (lightOff && lookingAtEnemy)
        {
            totalMindDrain += 4f;
        }

        if (totalMindDrain > 0)
        {
            mindBar.DecreaseMind(totalMindDrain * Time.deltaTime);
        }

        if (mindBar.mindValue <= 0)
        {
            player.TakeDamage(0.1f * Time.deltaTime);
        }
    }
}
