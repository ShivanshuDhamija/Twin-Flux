using System;
using UnityEngine;

public abstract class GenericEventChannelSO<T> : ScriptableObject where T : struct
{
    public event Action<T> OnEventInvoked;
    public void Invoke(T value)
    {
        OnEventInvoked?.Invoke(value);
    }
}
