using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    public Transform player;
    internal float intensity;

    private void LateUpdate()
    {
        if (player != null) transform.position = new Vector3(player.position.x, player.position.y, player.position.z);

    }
}
