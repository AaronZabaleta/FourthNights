using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadnessScreenEffect : MonoBehaviour
{
    [SerializeField] private CanvasGroup greenVignette;
    [SerializeField] private float maxAlpha = 0.54f; 
    [SerializeField] private float fadeSpeed = 2f;
    [SerializeField] private float mindValue = 100f;

    private void Update()
    {
        if (greenVignette == null) return;

        float mindNormalized = Mathf.Clamp01(mindValue / 100f);
        float alpha = Mathf.Lerp(0, maxAlpha, 1f - mindNormalized);
        greenVignette.alpha = Mathf.MoveTowards(greenVignette.alpha, alpha, Time.deltaTime * fadeSpeed);
    }

    public void SetMindValue(float value)
    {
        mindValue = value;
    }
}
