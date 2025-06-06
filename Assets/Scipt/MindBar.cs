using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MindBar : MonoBehaviour
{
    public Image mindState;
    public Slider mindSlider;
    public float mindValue = 100f;

    public void DecreaseMind(float amount)
    {
        mindValue = Mathf.Clamp(mindValue - amount, 0f, 100f);
        mindSlider.value = mindValue;
    }

    public void ResetMind()
    {
        mindValue = 100f;
        mindSlider.value = mindValue;
    }
}