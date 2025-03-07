using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventListener<T>
{
    void RaiseEvent(T parameter);
}
public abstract class BaseGameEventListener<TParameter, TGameEvent, TUnityEvent> : MonoBehaviour, IEventListener<TParameter>
    where TGameEvent : BaseGameEvent<TParameter>
    where TUnityEvent : UnityEvent<TParameter>
{
    [SerializeField] private TGameEvent gameEvent;
    [SerializeField] private TUnityEvent response;

    private void OnEnable()
    {
        if (gameEvent != null)
        {
            gameEvent.RegisterListener(this);
        }
    }

    private void OnDisable()
    {
        if (gameEvent != null)
        {
            gameEvent.UnregisterListener(this);
        }
    }

    public void RaiseEvent(TParameter parameter)
    {
        if (response != null)
        {
            response.Invoke(parameter);
        }
    }
}