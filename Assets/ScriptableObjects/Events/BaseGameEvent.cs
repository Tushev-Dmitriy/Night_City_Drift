using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public abstract class BaseGameEvent<TParameter> : ScriptableObject
{
    private List<IEventListener<TParameter>> _listeners = new List<IEventListener<TParameter>>();

    public void Raise(TParameter t)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            if (_listeners[i] != null)
            {
                _listeners[i].RaiseEvent(t);
            }
        }
    }

    public void RegisterListener(IEventListener<TParameter> listener)
    {
        if (listener != null && !_listeners.Contains(listener))
        {
            _listeners.Add(listener);
        }
    }

    public void UnregisterListener(IEventListener<TParameter> listener)
    {
        if (listener != null && _listeners.Contains(listener))
        {
            _listeners.Remove(listener);
        }
    }
}