using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent<T> : MonoBehaviour
{
    public event Action<T> OnEventRaised;

    public void Raise(T data)
    {
        OnEventRaised?.Invoke(data);
    }
}