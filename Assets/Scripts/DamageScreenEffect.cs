using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScreenEffect : MonoBehaviour
{
    [SerializeField] private CanvasGroup blueOverlay;
    [SerializeField] private float maxAlpha = 0.54f;
    [SerializeField] private float fadeSpeed = 2f;
    [SerializeField] private HealthComponent playerHealth;

    private bool isDead = false;

    private void Start()
    {
        if (playerHealth == null)
        {
            var player = GameObject.FindWithTag("Player");
            if (player != null)
                playerHealth = player.GetComponent<HealthComponent>();
        }
    }

    private void Update()
    {
        if (playerHealth == null || blueOverlay == null || isDead) return;

        float alpha = Mathf.Lerp(0, maxAlpha, 1f - playerHealth.NormalizedHealth);
        blueOverlay.alpha = Mathf.MoveTowards(blueOverlay.alpha, alpha, Time.deltaTime * fadeSpeed);
    }

    public void OnDeath()
    {
        isDead = true;
        if (blueOverlay != null)
            blueOverlay.alpha = maxAlpha; 
    }
}
