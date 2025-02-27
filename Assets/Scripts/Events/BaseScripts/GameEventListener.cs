using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] public GameEvent gameEvent;
    [SerializeField] public UnityEvent<object> response;

    private void OnEnable()
    {
        if (gameEvent != null) gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (gameEvent != null) gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(object data)
    {
        response.Invoke(data);
    }
}