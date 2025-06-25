using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadnessScreenEffect : MonoBehaviour
{
    [SerializeField] private CanvasGroup greenVignette;
    [SerializeField] private float maxAlpha = 0.54f;
    [SerializeField] private float fadeSpeed = 2f;
    [SerializeField] private float mindValue = 100f;

    private bool greenEffectActive = false;

    private void Update()
    {
        if (greenVignette == null) return;

        float targetAlpha = 0f;

        if (greenEffectActive)
        {
            float mindNormalized = Mathf.Clamp01(mindValue / 100f);
            targetAlpha = Mathf.Lerp(0, maxAlpha, 1f - mindNormalized);
        }

        greenVignette.alpha = Mathf.MoveTowards(greenVignette.alpha, targetAlpha, Time.deltaTime * fadeSpeed);
    }

    public void SetMindValue(float value)
    {
        mindValue = value;
    }

    public void SetGreenEffect(bool active)
    {
        greenEffectActive = active;
    }
}
