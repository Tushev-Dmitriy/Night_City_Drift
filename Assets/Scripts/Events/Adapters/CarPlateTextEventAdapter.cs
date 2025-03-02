using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarPlateTextEventAdapter : MonoBehaviour
{
    [SerializeField] private GameEvent carPlateTextChangeEvent;
    [SerializeField] private EventManager eventManager;

    private void OnEnable()
    {
        if (carPlateTextChangeEvent != null)
        {
            GameEventListener listener = gameObject.AddComponent<GameEventListener>();
            listener.gameEvent = carPlateTextChangeEvent;
            listener.response = new UnityEvent<object>();
            listener.response.AddListener(OnItemPurchase);
            carPlateTextChangeEvent.RegisterListener(listener);
        }
        else
        {
            Debug.LogError("carPlateTextChangeEvent не инициализирован!");
        }
    }

    private void OnDisable()
    {
        if (carPlateTextChangeEvent != null)
        {
            GameEventListener listener = gameObject.AddComponent<GameEventListener>();
            listener.gameEvent = carPlateTextChangeEvent;
            listener.response = new UnityEvent<object>();
            listener.response.AddListener(OnItemPurchase);
            carPlateTextChangeEvent.UnregisterListener(listener);
        }
    }

    private void OnItemPurchase(object data)
    {
        if (eventManager != null && data != null)
        {
            if (data is string text)
            {
                eventManager.OnCarPlateChange(text);
            }
            else
            {
                Debug.LogError("ѕолучен некорректный тип данных: " + data.GetType().Name);
            }
        }
        else
        {
            Debug.LogError("eventManager или data не инициализированы!");
        }
    }
}
