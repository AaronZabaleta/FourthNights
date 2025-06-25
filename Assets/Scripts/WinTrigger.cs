using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var pauseManager = FindObjectOfType<PauseManager>();
            if (pauseManager != null)
            {
                pauseManager.TriggerWin();
            }
        }
    }
}
