using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisibilityChecker : MonoBehaviour
{
    public Transform playerCamera;
    public LayerMask obstructionMask;
    public float visibilityThreshold = 0.5f;
    public float samplePoints = 5;
    private AudioSource audioSource;
    private bool wasVisible = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        bool isVisible = IsPartiallyVisible();

        if (isVisible && !wasVisible)
        {
            audioSource.Play();
        }
        else if (!isVisible && wasVisible)
        {
            audioSource.Stop();
        }

        wasVisible = isVisible;
    }
    public bool IsPartiallyVisible()
    {
        SkinnedMeshRenderer renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        if (renderer == null) return false;

        int visibleCount = 0;
        Vector3[] samplePositions = new Vector3[(int)samplePoints];

        
        Bounds bounds = renderer.bounds;

        for (int i = 0; i < samplePoints; i++)
        {
            Vector3 point = bounds.center + new Vector3(
                Random.Range(-bounds.extents.x, bounds.extents.x),
                Random.Range(-bounds.extents.y, bounds.extents.y),
                Random.Range(-bounds.extents.z, bounds.extents.z)
            );

            Vector3 dir = point - playerCamera.position;
            if (!Physics.Raycast(playerCamera.position, dir, dir.magnitude, obstructionMask))
            {
                visibleCount++;
            }
        }

        float visibleRatio = visibleCount / samplePoints;
        return visibleRatio >= visibilityThreshold;
    }
}
