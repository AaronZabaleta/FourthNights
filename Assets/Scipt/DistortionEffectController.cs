using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DistortionEffectController : MonoBehaviour
{
    public PostProcessVolume volume;
    private ChromaticAberration chromatic;
    private Grain grain;
    private LensDistortion distortion;

    public float effectLerpSpeed = 5f;

    void Start()
    {
        volume.profile.TryGetSettings(out chromatic);
        volume.profile.TryGetSettings(out grain);
        volume.profile.TryGetSettings(out distortion);
    }

    public void SetDistortionActive(bool active)
    {
        float target = active ? 1f : 0f;

        chromatic.intensity.value = Mathf.Lerp(chromatic.intensity.value, target, Time.deltaTime * effectLerpSpeed);
        grain.intensity.value = Mathf.Lerp(grain.intensity.value, target * 0.8f, Time.deltaTime * effectLerpSpeed);
        distortion.intensity.value = Mathf.Lerp(distortion.intensity.value, target * -20f, Time.deltaTime * effectLerpSpeed);
    }
}