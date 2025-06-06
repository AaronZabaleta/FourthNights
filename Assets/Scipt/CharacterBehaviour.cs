using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [Header("Entity")]
    [SerializeField] protected int _maxHp;

    protected bool _isAlive = true;
    protected int _actualHp;

    protected virtual void Awake()
    {

        _actualHp = _maxHp;

    }

    protected virtual void Start()
    {



    }

    protected virtual void Update()
    {
        if (!_isAlive) return;
    }

    protected virtual void FixedUpdate()
    {
        if (!_isAlive) return;
    }

    protected virtual void LateUpdate()
    {
        if (!_isAlive) return;
    }

    public virtual void TakeDamage(float dmg)
    {
        _actualHp -= Mathf.RoundToInt(dmg);
        if (_actualHp <= 0) _isAlive = false;
    }

    public float GetNormalizedLife()
    {
        return Mathf.Clamp01((float)_actualHp / _maxHp);
    }
}
