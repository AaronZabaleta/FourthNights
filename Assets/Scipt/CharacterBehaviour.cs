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

    public virtual void TakeDamage(int dmg)
    {
        _actualHp -= dmg;

        if(_actualHp <= 0)
        {
            _isAlive = false;

        }
      
    }
}
