using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindController : MonoBehaviour
{
    public MindBar mindBar;
    public EnemyVisibilityChecker enemyChecker;
    public Transform playerCamera;
    public PlayerLightController lightController;
    public LifeBar healthBar;

    public float mindDrainFromEnemy = 5f;
    public float mindDrainFromDarkness = 3f;

    void Update()
    {


        bool lightOff = lightController.IsLightOff;

        // El jugador está viendo al enemigo?
        Vector3 dirToEnemy = (enemyChecker.transform.position - playerCamera.position).normalized;
        float dot = Vector3.Dot(playerCamera.forward, dirToEnemy);
        bool lookingAtEnemy = dot > 0.7f && enemyChecker.IsPartiallyVisible();
        Debug.Log($"Visible: {lookingAtEnemy} | Luz apagada: {lightOff} | Mente: {mindBar.mindValue}");

        float totalMindDrain = 0f;

        //  Mente baja si cualquiera de las condiciones se cumple
        if (lookingAtEnemy)
        {
            totalMindDrain += mindDrainFromEnemy;
            Debug.Log("Enemy visible: " + enemyChecker.IsPartiallyVisible());
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

       


        //  Si la mente está en cero, bajar salud
        if (mindBar.mindValue <= 0)
        {
            healthBar.DecreaseHealth(1f);
        }
    }
}
