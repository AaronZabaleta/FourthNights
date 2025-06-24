using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHp = 100;
    [SerializeField] private int currentHp;

    public bool IsAlive => currentHp > 0;
    public int CurrentHealth => currentHp;
    public float NormalizedHealth => Mathf.Clamp01((float)currentHp / maxHp);

    public HealthEvent onHealthChanged;
    public UnityEvent onDeath;

    private void Awake()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int dmg)
    {
        currentHp -= Mathf.RoundToInt(dmg);
        currentHp = Mathf.Max(0, currentHp);

        Debug.Log("Vida restante: " + currentHp); 

        onHealthChanged?.Raise(currentHp);

        if (currentHp == 0)
            onDeath?.Invoke();
    }
}