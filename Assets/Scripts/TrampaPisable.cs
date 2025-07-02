using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaPisable : MonoBehaviour
{
    public Flecha flechaScript;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            flechaScript.Disparo();
            GameAudioManager.Instance?.PlaySFX(GameAudioManager.Instance.buttonPressClip);
            Debug.Log("Flecha disparada desde trampa");
        }
    }
}
