using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoneyIntEventAdapter : MonoBehaviour
{
    [SerializeField] private GameEvent purchaseItemEvent;
    [SerializeField] private EventManager eventManager;

    private void OnEnable()
    {
        if (purchaseItemEvent != null)
        {
            GameEventListener listener = gameObject.AddComponent<GameEventListener>();
            listener.gameEvent = purchaseItemEvent;
            listener.response = new UnityEvent<object>();
            listener.response.AddListener(OnItemPurchase);
            purchaseItemEvent.RegisterListener(listener);
        }
        else
        {
            Debug.LogError("purchaseItemEvent �� ���������������!");
        }
    }

    private void OnDisable()
    {
        if (purchaseItemEvent != null)
        {
            GameEventListener listener = gameObject.AddComponent<GameEventListener>();
            listener.gameEvent = purchaseItemEvent;
            listener.response = new UnityEvent<object>();
            listener.response.AddListener(OnItemPurchase);
            purchaseItemEvent.UnregisterListener(listener);
        }
    }

    private void OnItemPurchase(object data)
    {
        if (eventManager != null && data != null)
        {
            if (data is int price)
            {
                eventManager.OnPurchaseItem(price);
            }
            else
            {
                Debug.LogError("������� ������������ ��� ������: " + data.GetType().Name);
            }
        }
        else
        {
            Debug.LogError("eventManager ��� data �� ����������������!");
        }
    }
}
